using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmartSchool.WebAPI.Helpers
{
    // PAGINAÇÃO
    // Como será paginado Varios Itens na Paginação, usando (T) -> Genérico
    public class PageList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PageList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            /* Imaginando que cada pagina pussua 5 itens e teria 3 paginas então abaixo a técnica para calculo
            que Ira distribuir por exemplo 13 items em 03 paginas, sendo duas paginas com 05 itens e uma pagina
            com 03 itens...*/
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items); // AddRanger -> Adiciona todos.
        }

        public static async Task<PageList<T>> CreateAsync(            
             IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            // Calculo para pular a quantidade de paginas de acordo com os itens de cada pagina
            var items = await source.Skip((pageNumber-1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
            return new PageList<T>(items, count, pageNumber, pageSize);
        }
        
    }
}