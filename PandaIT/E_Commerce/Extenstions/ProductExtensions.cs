using E_Commerce.Models.Domain;
namespace E_Commerce.Extenstions
{
    public static class ProductExtensions
    {
        public static IQueryable<Product> Sort(this IQueryable<Product> query, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(p => p.Id);

            query = orderBy switch
            {
                "price" => query.OrderBy(p => p.Price),
                "priceDesc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(n => n.Name)
            };

            return query;
        }

        //public static IQueryable<Product> Search(this IQueryable<Product> query, string searchTerm, int maxDistance=2)
        //{
        //    if (string.IsNullOrEmpty(searchTerm)) return query;

        //    var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

        //    return query.Where(p => 
        //    LevenshteinDistance(p.Name.ToLower(), lowerCaseSearchTerm) <= maxDistance ||
        //    LevenshteinDistance(p.Brand.ToLower(), lowerCaseSearchTerm) <= maxDistance ||
        //    LevenshteinDistance(p.Type.ToLower(), lowerCaseSearchTerm) <= maxDistance

        //    );
        //}
        public static IQueryable<Product> Search(this IQueryable<Product> query, string searchTerm, int maxDistance=2)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return query;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return query.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTerm)||
            p.Brand.ToLower().Contains(lowerCaseSearchTerm)||
            p.Type.ToLower().Contains(lowerCaseSearchTerm)||
            p.Name.ToLower().StartsWith(lowerCaseSearchTerm)
            );

        }

        public static IQueryable<Product> Filter(this IQueryable<Product> query, string brand, string type)
        {
            var brandList = new List<string>();
            var typeList = new List<string>();

            if (!string.IsNullOrEmpty(brand))
                brandList.AddRange(brand.ToLower().Split(",").ToList());

            if (!string.IsNullOrEmpty(type))
                typeList.AddRange(type.ToLower().Split(",").ToList());

            query = query.Where(p => brandList.Count == 0 || brandList.Contains(p.Brand.ToLower()));

            query = query.Where(p => typeList.Count == 0 || typeList.Contains(p.Type.ToLower()));

            return query;
        }

        private static int LevenshteinDistance(string s1, string s2)
        {
            int[,] distance = new int[s1.Length + 1, s2.Length + 1];

            for (int i = 0; i <= s1.Length; i++)
                distance[i, 0] = i;

            for (int j = 0; j <= s2.Length; j++)
                distance[0, j] = j;

            for (int i = 1; i <= s1.Length; i++)
            {
                for (int j = 1; j <= s2.Length; j++)
                {
                    int cost = (s2[j - 1] == s1[i - 1]) ? 0 : 1;
                    distance[i, j] = Math.Min(
                        Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                        distance[i - 1, j - 1] + cost
                    );
                }
            }

            return distance[s1.Length, s2.Length];
        }

    }

}
