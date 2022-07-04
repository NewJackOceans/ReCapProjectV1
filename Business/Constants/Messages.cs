using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

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


        public static string MaintenanceTime = " System maintenance!";
        public static string BrandAdded = "Brand successfully added";
        public static string BrandUpdated = "Brand successfully updated";
        public static string BrandDeleted = "Brand successfully deleted";
        public static string BrandsListed = "Brands listed!";

        public static string ColorAdded = "Color successfully added";
        public static string ColorUpdated = "Color successfully updated";
        public static string ColorDeleted = "Color successfully deleted";
        public static string ColorsListed = "Colors listed!";

        public static string UserAdded = "User successfully added";
        public static string UserUpdated = "User successfully updated";
        public static string UserDeleted = "User successfully deleted";
        public static string UsersListed = "Users listed";

        public static string RentalAdded = "Rental successfully added";
        public static string RentalUpdated = "Rental successfully updated";
        public static string RentalDeleted = "Rental successfully deleted";
        public static string RentalsListed = "Rentals listed";

        public static string CustomerAdded = "Customer successfully added";
        public static string CustomerUpdated = "Customer successfully updated";
        public static string CustomerDeleted = "Customer successfully deleted";
        public static string CustomersListed = "Customers listed";

        public static string CarRented = "The car has not been received yet.Please check for another car.";

        public static string CarImageAdded = "Car image successfully added";

        public static string AuthorizationDenied = "Authorization denied";
        public static string UserRegistered = "User registered";
        public static string UserNotFound = "User not found";
        public static string PasswordError = "Password error";
        public static string SuccessfulLogin = "Successful login";
        public static string UserAlreadyExists = "User already exists";
        public static string AccessTokenCreated = "Access token created";

        public static string BrandPaging = "Brand Paging";

        public static string CardPaging { get; internal set; }
        public static string CarImagePaging { get; internal set; }
        public static string ColorPaging { get; internal set; }
        public static string CustomerPaging { get; internal set; }
        public static string PaymentPaging { get; internal set; }
        public static string RentalPaging { get; internal set; }
        public static string UserPaging { get; internal set; }
    }
}