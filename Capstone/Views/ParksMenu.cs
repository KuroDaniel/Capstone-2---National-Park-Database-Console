using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;

namespace Capstone.Views
{
    /// <summary>
    /// The top-level menu in our Market Application
    /// </summary>
    public class ParksMenu : MainMenu
    {
        // Store any private variables, including DAOs here....
        private Park selectedPark;

        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public ParksMenu(Park selectedPark, ICampgroundSqlDAO campgroundDAO, IParkSqlDAO parkDAO, IReservationSqlDAO reservationDAO, ISiteSqlDAO siteDAO) :
            base(campgroundDAO, parkDAO, reservationDAO, siteDAO)
        {
            this.selectedPark = selectedPark;
        }

        protected override void SetMenuOptions()
        {
            this.menuOptions.Add("1", "View Campgrounds");
            this.menuOptions.Add("2", "Search for Reservation");
            this.menuOptions.Add("3", "Return to Previous Screen");
            //this.quitKey = "3";
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
                case "1": // Go to new SubMenu for Park Campgrounds
                    //WriteError("Not yet implemented");
                    CampgroundsMenu cm = new CampgroundsMenu(selectedPark, campgroundDAO, parkDAO, reservationDAO, siteDAO);
                    cm.Run();
                    Pause("");
                    return true;
                case "2": // Go to SubMenu for Reservations
                    ReservationsMenu rm = new ReservationsMenu(selectedPark, campgroundDAO, parkDAO, reservationDAO, siteDAO);
                    rm.Run();
                    Pause("");
                    return true;
                case "3":
                    MainMenu mm = new MainMenu(campgroundDAO, parkDAO, reservationDAO, siteDAO);
                    mm.Run();
                    Pause("");
                    return true;
            }
            return true;
        }

        protected override void BeforeDisplayMenu()
        {
            PrintHeader();
            SetColor(ConsoleColor.Cyan);
            Console.WriteLine($"{selectedPark.Name} National Park");
            Console.WriteLine($"Location: {selectedPark.Location}");
            Console.WriteLine($"Established: {selectedPark.EstablishDate.ToString("d")}");
            Console.WriteLine($"Area: {selectedPark.Area.ToString("N0")} sq km");
            Console.WriteLine($"Annual Visitors: {selectedPark.Visitors.ToString("N0")}");
            Console.WriteLine();
            Console.WriteLine($"{selectedPark.Description}");
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
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Park Information"));
            ResetColor();
        }

    }
}
