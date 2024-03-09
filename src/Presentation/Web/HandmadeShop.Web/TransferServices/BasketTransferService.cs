using HandmadeShop.SharedModel.Basket.Models;

namespace HandmadeShop.Web.TransferServices;

public class BasketTransferService
{
    private BasketModel _basketModel;

    public BasketModel Data
    {
        get => _basketModel;
        set
        {
            _basketModel = value;
            DataChanged.Invoke(this, value);
        }
    }
    
    public event EventHandler<BasketModel> DataChanged = (sender, value) => { };
}