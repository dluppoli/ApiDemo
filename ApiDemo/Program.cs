using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace ApiDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //var client = new RestClient(@"https://api.pwnedpasswords.com/");
            //var request = new RestRequest(@"range/{range}");
            //request.AddParameter("range","7c4a8",ParameterType.UrlSegment);
            //var response = await client.GetAsync(request);

            //if( response.IsSuccessful)
            //{
            //    var risultato = response.Content;
            //    var risultati = risultato.Split('\n');

            //    var x = risultati.FirstOrDefault(s => s.ToLower().Contains("d09ca3762af61e59520943dc26494f8941b"));
            //    if( x != null )
            //    {
            //        Console.WriteLine(x.Substring(36));
            //    }
            //}

            /*try
            {
                var client = new RestClient(@"https://api.isevenapi.xyz/api/iseven/{numero}");
                var request = new RestRequest();
                request.AddParameter("numero", "12", ParameterType.UrlSegment);
                var response = await client.GetAsync<IsEvenApiResponse>(request);

                Console.WriteLine(response.ad);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore");
            }*/


            try
            {
                var client = new RestClient(@"https://jsonplaceholder.typicode.com/posts");
                //var client = new RestClient(@"https://webhook.site/69e136c7-6226-4601-be27-e98ef7560c23");
                var req = new RestRequest();

                var newPost = new Post
                {
                    UserId = 12,
                    Title = "Nuovo Post",
                    Body = "Body del nuovo post"
                };

                req.AddJsonBody<Post>(newPost);
                var resp = await client.PostAsync(req);
                Console.WriteLine(resp.IsSuccessful);
            }
            catch (Exception ex)
            {

            }
            Console.ReadLine();
        }
    }
}
