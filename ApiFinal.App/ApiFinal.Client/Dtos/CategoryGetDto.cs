namespace ApiFinal.Client.Dtos
{
    public class CategoryGetDto
    {
        public string Name { get; set; }
    }
    public class GetItems<T> 
    {
        public List<T> Items { get; set; }
    }

}
