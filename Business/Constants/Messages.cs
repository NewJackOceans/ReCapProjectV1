using Entities.Concrete;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarAdded = "Car successfully Added!";
        public static string CarUpdated = "Car successfully updated!";
        public static string CarDeleted = "Car successfully deleted!";
        public static string CarCondition = "Car description must be at least 2 characters or car daily price must be greater than 0. ";
        public static string CarsListed = "Car listed!";
        public static string CarDetailsListed = "Car details listed!";
        public static string CarPaging = "Car paging.";
        public static string CarNotUpdated = "Car not updated.";


        public static string MaintenanceTime = " System maintenance!";
        public static string BrandAdded = "Brand successfully added";
        public static string BrandUpdated = "Brand successfully updated";
        public static string BrandNotUpdated = "Brand Not Updated";
        public static string BrandDeleted = "Brand successfully deleted";
        public static string BrandsListed = "Brands listed!";
        public static string BrandPaging = "Brand Paging";
        public static string NotFoundBrand = "Not Find Brand.";
        public static string BrandIdNotAvailable = "Brand Is Not Available.";
        public static string BrandIdIsAvailable = "BrandId Is Available.";
        public static string BrandNameIsAvailable = "Brand Name is Available.";


        public static string ColorAdded = "Color successfully added";
        public static string ColorUpdated = "Color successfully updated";
        public static string ColorDeleted = "Color successfully deleted";
        public static string ColorsListed = "Colors listed!";
        public static string ColorNotUpdated = "Color Not Updated";
        public static string ColorPaging = "Color Paging.";
        public static string NotFoundColor = "Not Found Color.";
        public static string ColorNameIsAvailable = "Color Name is Available.";


        public static string UserAdded = "User successfully added";
        public static string UserUpdated = "User successfully updated";
        public static string UserDeleted = "User successfully deleted";
        public static string UsersListed = "Users listed";
        public static string UserPaging = "User Paging.";

        public static string PaymentUpdated = "Payment updated.";
        public static string PaymentNotUpdated = "Payment not updated.";
        public static string PaymentPaging = "Payment Paging";

        public static string RentalAdded = "Rental successfully added";
        public static string RentalUpdated = "Rental successfully updated";
        public static string RentalDeleted = "Rental successfully deleted";
        public static string RentalsListed = "Rentals listed";
        public static string RentalPaging = "Rental Paging.";
        public static string RentalNotUpdated = "Rental Not Updated.";

        public static string CustomerAdded = "Customer successfully added";
        public static string CustomerUpdated = "Customer successfully updated";
        public static string CustomerDeleted = "Customer successfully deleted";
        public static string CustomersListed = "Customers listed";
        public static string CustomerNotUpdated = "Customer not updated.";
        public static string CustomerPaging = "Customer Paging.";
        public static string NotFoundCustomer = "Customer Not Found";
        public static string CompanyNameIsAvailable = "Company Name is Available.";

        public static string CarRented = "The car has not been received yet.Please check for another car.";

        public static string CarImageAdded = "Car image successfully added";
        public static string CarImageDeleted = "Car Image Deleted.";
        public static string CarImageNotUpdated = "Car Image Not Updated";
        public static string CarImageUpdated = "Car Image Updated";
        public static string CarImagePaging = "Car Image Paging.";

        public static string AuthorizationDenied = "Authorization denied";
        public static string UserRegistered = "User registered";
        public static string UserNotFound = "User not found";
        public static string UserNotUpdated = "User Not Updated.";
        public static string PasswordError = "Password error";
        public static string SuccessfulLogin = "Successful login";
        public static string UserAlreadyExists = "User already exists";
        public static string AccessTokenCreated = "Access token created";
        public static string UserIsAvailable = "User is Available.";

        public static string CardUpdated = "Card Updated.";
        public static string CardNotUpdated = "Card Not Updated.";
        public static string CardPaging = "Card Paging.";

        public static string CarNotFound = "Car Not Found";

        public static string RentDateCannotBeGreaterThanReturnDate = "RentDate can not be greater than ReturrnDate";

        public static string CarServiceListed = "Car Service Listed.";
        public static string CarServicePaging = "Car Service Paging.";
        public static string CarServiceNotFound = "Car Service Not Found.";
        public static string NotFoundCarService = " Not Found Car Service.";
        public static string CarServiceNotUpdated = "Car Service Not Updated.";
        public static string CarServiceDeleted = "Car Service Deleted.";
        public static string CarServiceNotDeleted = "Car Service Not Deleted.";
        public static string CarServiceAdded = "Car Service Added.";
        public static string ServiceEntryCannotBeGreaterThanServiceExitDate = "Car Service Exit can not be greater than Service Entry Date";

        public static string TyreListed = "Tyre Listed";
        public static string TyreCategoryListed = "Tyre Category Listed";
        public static string TyreBrandListed = "Tyre Brand Listed";
        public static string CarTyreChangeListed = "Car Tyre Change Listed";
        public static string TyreNotFound = "Tyre Not Found";
        public static string TyreBrandNotFound = "Tyre Brand Not Found";
        public static string TyreAdded = "Tyre Added";
        public static string TyreCategoryAdded = "Tyre Category Added";
        public static string TyreBrandAdded = "Tyre Brand Added";
        public static string CarTyreChangeAdded = "Car Tyre Change Added";
        public static string TyreDeleted = "Tyre Deleted";
        public static string TyreCategoryDeleted = "Tyre Category Deleted";
        public static string TyreBrandDeleted = "Tyre Brand Deleted";
        public static string CarTyreChangeDeleted = "Car Tyre Change Deleted";
        public static string TyreUpdated = "Tyre Updated";
        public static string TyreNotUpdated = "Tyre Not Updated";
        public static string TyreCategoryUpdated = "Tyre Category Updated";
        public static string TyreCategoryNotUpdated = "Tyre Category Not Updated";
        public static string TyreBrandUpdated = "Tyre Brand Updated";
        public static string TyreBrandNotUpdated = "Tyre Brand Not Updated";
        public static string TyreCarChangeUpdated = "Tyre Car Change Updated";
        public static string TyreCarChangeNotUpdated = "Tyre Car Change Not Updated";
        public static string AmountNotGreatherThanFour = "Tyres cannot be more than 4";

        public static string TyreIdsNotGretherThan4 { get; internal set; }
        public static string TyreNameNotFound { get; internal set; }
        public static object NotFoundTrueMail { get; internal set; }
    }
}