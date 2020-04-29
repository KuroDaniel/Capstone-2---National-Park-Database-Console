using Capstone.DAL;
using Capstone.Models;
using System;
using System.Globalization;
using System.Collections.Generic;

namespace Capstone.Views
{
    public class CampgroundsMenu : MainMenu
    {
        // Store any private variables, including DAOs here....
        private Park selectedPark;
        private string menuNumber;

        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public CampgroundsMenu(Park selectedPark, ICampgroundSqlDAO campgroundDAO, IParkSqlDAO parkDAO, IReservationSqlDAO reservationDAO, ISiteSqlDAO siteDAO) :
            base(campgroundDAO, parkDAO, reservationDAO, siteDAO)
        {
            this.selectedPark = selectedPark;
        }

        protected override void SetMenuOptions()
        {
            this.menuOptions.Add("1", "Search for Available Reservation");
            this.menuOptions.Add("2", "Return to Previous Screen");
        }

        /// <summary>
        /// The override of ExecuteSelection handles whatever selection was made by the user.
        /// This is where any business logic is executed.
        /// </summary>
        /// <param name="choice">"Key" of the user's menu selection</param>
        /// <returns></returns>
        protected override bool ExecuteSelection(string choice)
        {
            switch (choice)
            {
                case "1": // Go to new SubMenu for Reservations
                    //WriteError("Not yet implemented");
                    ReservationsMenu rm = new ReservationsMenu(selectedPark, campgroundDAO, parkDAO, reservationDAO, siteDAO);
                    rm.Run();
                    Pause("");
                    return true;
                case "2":
                    ParksMenu pm = new ParksMenu(selectedPark, campgroundDAO, parkDAO, reservationDAO, siteDAO);
                    pm.Run();
                    Pause("");
                    return true;
            }
            return true;
        }

        protected override void BeforeDisplayMenu()
        {
            PrintHeader();
            SetColor(ConsoleColor.Red);
            IList<Campground> campgrounds = campgroundDAO.GetCampgroundsByParkId(selectedPark.ParkId);
            int sum = 0;
            Console.WriteLine($"      {"Name",-10} {"Open",27} {"Close",11} {"Daily Fee",16}");
            foreach (Campground campground in campgrounds)
            {
                sum += 1;
                menuNumber = Convert.ToString(sum);
                string openFromMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(campground.OpenFromMonth);
                string openToMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(campground.OpenToMonth);
                Console.WriteLine($"{"#"}{menuNumber}    {campground.Name, -33} {openFromMonth, -10} {openToMonth, -12} {campground.DailyFee.ToString("C"), 0}");

            }
            ResetColor();
        }

        protected override void AfterDisplayMenu()
        {
            base.AfterDisplayMenu();
            SetColor(ConsoleColor.Green);
            //Console.WriteLine("Display some data here, AFTER the sub-menu is shown....");
            ResetColor();
        }

        private void PrintHeader()
        {
            SetColor(ConsoleColor.Magenta);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Campgrounds"));
            ResetColor();
        }
    }
}