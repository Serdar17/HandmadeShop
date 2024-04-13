namespace HandmadeShop.Web.TransferServices;

public class SearchValueTransferService
{
    private string _searchValue;

    public string Data
    {
        get => _searchValue;
        set
        {
            _searchValue = value;
            DataChanged.Invoke(this, value);
        }
    }
    
    public event EventHandler<string> DataChanged = (sender, value) => { };
}