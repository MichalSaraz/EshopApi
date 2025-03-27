namespace EshopApi.Shared.Models
{
    public class QueryStringParameters
{
    private const int MaxPageSize = 50;

    private int _pageNumber = 1;
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = (value < 1) ? 1 : value; // Zajišťuje, že je číslo kladné
    }

    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value < 1 || value > MaxPageSize) ? 10 : value; // Omezíme velikost stránky
    }

    public bool IsValid(out string? errorMessage)
    {
        errorMessage = null;

        if (PageNumber < 1)
        {
            errorMessage = "PageNumber must be greater than 0.";
            return false;
        }

        if (PageSize < 1 || PageSize > MaxPageSize)
        {
            errorMessage = $"PageSize must be between 1 and {MaxPageSize}.";
            return false;
        }

        return true;
    }
}
}