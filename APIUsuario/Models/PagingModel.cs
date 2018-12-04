namespace APIUsuario.Models
{  
    public class PagingModel
    {  
        const int maxItemsPerPage = 50;
    
        public int pageNumber { get; set; } = 1;
    
        public int _itemsPerPage { get; set; } = 20;
    
        public int itemsPerPage
        {
    
            get { return _itemsPerPage; }
            set
            {
                _itemsPerPage = (value > maxItemsPerPage) ? maxItemsPerPage : value;
            }
        }
    }  
}  