
    namespace APIUsuario.Models  
    {  
        public class PagingModel
        {  
            const int maxPageSize = 50;  
      
            public int pageNumber { get; set; } = 1;  
      
            public int _pageSize { get; set; } = 20;  
      
            public int pageSize  
            {  
      
                get { return _pageSize; }  
                set  
                {  
                    _pageSize = (value > maxPageSize) ? maxPageSize : value;  
                }  
            }  
        }  
    }  