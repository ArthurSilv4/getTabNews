using getTabNews.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics;
using System.Net.Http.Json;

namespace getTabNews.Endpoints
{
    public static class PostEndpoint
    {
        public static void MapPostEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/posts", async (
                IHttpClientFactory httpClientFactory, 
                ILoggerFactory loggerFactory,
                string filter = "[filtro]") =>
            {
                var logger = loggerFactory.CreateLogger("PostEndpoint");
                var stopwatch = Stopwatch.StartNew();
                
                try
                {
                    logger.LogInformation("Iniciando busca de posts com filtro: {Filter}", filter);
                    
                    var client = httpClientFactory.CreateClient("TabNews");
                    var posts = await GetPostDetailsAsync(client, filter, logger);
                    
                    stopwatch.Stop();
                    
                    if (!posts.Any())
                    {
                        logger.LogWarning("Nenhum post encontrado com o filtro: {Filter}", filter);
                        return Results.NotFound("Nenhum post encontrado com o filtro especificado.");
                    }
                    
                    logger.LogInformation("Busca de posts concluída com sucesso. {Count} posts encontrados em {ElapsedMs}ms", 
                        posts.Count(), stopwatch.ElapsedMilliseconds);
                        
                    return Results.Ok(posts);
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();
                    logger.LogError(ex, "Erro ao buscar posts com filtro {Filter}: {Message}", 
                        filter, ex.Message);
                    
                    return Results.Problem($"Erro ao processar a requisição: {ex.Message}");
                }
            })
            .WithName("GetFilteredPosts")
            .WithDescription("Obtém posts filtrados por prefixo no título")
            .WithOpenApi();
        }

        private static async Task<IEnumerable<FilterPostsDto>> GetFilteredPostsAsync(
            HttpClient client, 
            string filter,
            ILogger logger)
        {
            logger.LogDebug("Buscando posts iniciais da API TabNews");
            
            var posts = await client.GetFromJsonAsync<List<FilterPostsDto>>("/api/v1/contents?page=1&per_page=100&strategy=new");

            if (posts == null)
            {
                logger.LogWarning("API TabNews retornou uma lista nula de posts");
                return Enumerable.Empty<FilterPostsDto>();
            }

            var filteredPosts = posts
                .Where(p => !string.IsNullOrWhiteSpace(p.Title) && 
                           p.Title.Trim().ToLower().StartsWith(filter))
                .ToList();
                
            logger.LogInformation("Encontrados {FilteredCount} posts com filtro {Filter} de um total de {TotalCount}", 
                filteredPosts.Count, filter, posts.Count);
                
            return filteredPosts;
        }

        private static async Task<IEnumerable<PostDetailsDto>> GetPostDetailsAsync(
            HttpClient client, 
            string filter,
            ILogger logger)
        {
            var posts = await GetFilteredPostsAsync(client, filter, logger);
            var postDetails = new List<PostDetailsDto>();

            logger.LogInformation("Iniciando obtenção de detalhes para {Count} posts", posts.Count());

            foreach (var post in posts)
            {
                try
                {
                    using (logger.BeginScope("Post {Slug} de {Author}", post.Slug, post.OwnerUsername))
                    {
                        logger.LogDebug("Buscando detalhes do post: {Title}", post.Title);
                        
                        var url = $"/api/v1/contents/{post.OwnerUsername}/{post.Slug}";
                        var response = await client.GetAsync(url);
                        
                        response.EnsureSuccessStatusCode();
                        
                        var postDetail = await response.Content.ReadFromJsonAsync<PostDetailsDto>();
                        if (postDetail != null)
                        {
                            postDetails.Add(postDetail);
                            logger.LogDebug("Detalhes do post obtidos com sucesso");
                        }
                        else
                        {
                            logger.LogWarning("Desserialização do post retornou nulo");
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Erro ao obter detalhes do post {Slug} de {Author}: {Message}", 
                        post.Slug, post.OwnerUsername, ex.Message);
                }
            }

            logger.LogInformation("Concluída obtenção de {SuccessCount} de {TotalCount} posts detalhados", 
                postDetails.Count, posts.Count());

            return postDetails;
        }
    }
}
