using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;

namespace Capstone.Views
{
    /// <summary>
    /// The top-level menu in our Market Application
    /// </summary>
    public class MainMenu : CLIMenu
    {
        // DAOs - Interfaces to our data objects can be stored here...
        protected ICampgroundSqlDAO campgroundDAO;
        protected IParkSqlDAO parkDAO;
        protected IReservationSqlDAO reservationDAO;
        protected ISiteSqlDAO siteDAO;
        private string menuNumber;
        private Park selectedPark = new Park();
        private int input;

        /// <summary>
        /// Constructor adds items to the top-level menu. YOu will likely have parameters for one or more DAO's here...
        /// </summary>
        public MainMenu(ICampgroundSqlDAO campgroundDAO, IParkSqlDAO parkDAO, IReservationSqlDAO reservationDAO, ISiteSqlDAO siteDAO) : base("Main Menu")
        {
            this.campgroundDAO = campgroundDAO;
            this.parkDAO = parkDAO;
            this.reservationDAO = reservationDAO;
            this.siteDAO = siteDAO;
        }

        protected override void SetMenuOptions()
        {
            //this.menuOptions.Add("1", "Add 2 integers");
            //this.menuOptions.Add("2", "Menu option 2");
            //this.menuOptions.Add("3", "Go to a sub-menu");
            //this.menuOptions.Add("Q", "Quit program");
            SetColor(ConsoleColor.Yellow);
            IList<Park> parks = parkDAO.GetParks();
            int sum = 0;
            foreach (Park park in parks)
            {
                sum += 1;
                menuNumber = Convert.ToString(sum);
                this.menuOptions.Add($"{menuNumber}", $"{park.Name}");
            }
            this.menuOptions.Add("Q", "Quit");
            this.quitKey = "Q";
            ResetColor();
        }

        /// <summary>
        /// The override of ExecuteSelection handles whatever selection was made by the user.
        /// This is where any business logic is executed.
        /// </summary>
        /// <param name="choice">"Key" of the user's menu selection</param>
        /// <returns></returns>
        protected override bool ExecuteSelection(string choice)
        {
            Console.Clear();
            IList<Park> parks = parkDAO.GetParks();
            int chosen = Convert.ToInt32(choice);

            foreach(Park park in parks)
            {
                if (chosen == park.ParkId)
                {
                    selectedPark = park;
                    break;
                }
            }

            if (chosen <= parks.Count)
            {
                ParksMenu pm = new ParksMenu(selectedPark, campgroundDAO, parkDAO, reservationDAO, siteDAO);
                pm.Run();
                Pause("");
                return true;
            }
            //else if (chosen > parks.Count)
            //{
            //    Console.WriteLine("Please enter a valid menu option");
            //    Pause("");
            //    MainMenu mm = new MainMenu(campgroundDAO, parkDAO, reservationDAO, siteDAO);
            //    mm.Run();
            //    Pause("");
            //    return true;
            //}
            //else if (choice.ToUpper() == "Q")
            //{
            //    Environment.Exit(0);
            //}
            // TODO: Ask about having a message appear when not selecting a valid number
            #region Original way we approached getting to SubMenu1
            //switch (choice)
            //{
            //    case "1": // Do whatever option 1 is
            //        //Console.WriteLine($"{selectedPark.Name} National Park");
            //        //Console.WriteLine($"Location: {selectedPark.Location}");
            //        //Console.WriteLine($"Established: {selectedPark.EstablishDate.ToString("d")}");
            //        //Console.WriteLine($"Area: {selectedPark.Area.ToString("N0")} sq km");
            //        //Console.WriteLine($"Annual Visitors: {selectedPark.Visitors.ToString("N0")}");
            //        //Console.WriteLine();
            //        //Console.WriteLine($"{selectedPark.Description}");
            //        //Console.WriteLine();
            //        SubMenu1 sm = new SubMenu1(selectedPark, campgroundDAO, parkDAO, reservationDAO, siteDAO);
            //        sm.Run();
            //        Pause("");
            //        return true;    // Keep running the main menu
            //    case "2": // Do whatever option 2 is
            //        //Console.WriteLine($"{selectedPark.Name} National Park");
            //        //Console.WriteLine($"Location: {selectedPark.Location}");
            //        //Console.WriteLine($"Established: {selectedPark.EstablishDate.ToString("d")}");
            //        //Console.WriteLine($"Area: {selectedPark.Area.ToString("N0")} sq km");
            //        //Console.WriteLine($"Annual Visitors: {selectedPark.Visitors.ToString("N0")}");
            //        //Console.WriteLine();
            //        //Console.WriteLine($"{selectedPark.Description}");
            //        //Console.WriteLine();
            //        SubMenu1 sm2 = new SubMenu1(selectedPark, campgroundDAO, parkDAO, reservationDAO, siteDAO);
            //        sm2.Run();
            //        Pause("");
            //        return true;    // Keep running the main menu
            //    case "3": // Create and show the sub-menu
            //        //Console.WriteLine($"{selectedPark.Name} National Park");
            //        //Console.WriteLine($"Location: {selectedPark.Location}");
            //        //Console.WriteLine($"Established: {selectedPark.EstablishDate.ToString("d")}");
            //        //Console.WriteLine($"Area: {selectedPark.Area.ToString("N0")} sq km");
            //        //Console.WriteLine($"Annual Visitors: {selectedPark.Visitors.ToString("N0")}");
            //        //Console.WriteLine();
            //        //Console.WriteLine($"{selectedPark.Description}");
            //        //Console.WriteLine();
            //        SubMenu1 sm3 = new SubMenu1(selectedPark, campgroundDAO, parkDAO, reservationDAO, siteDAO);
            //        sm3.Run();
            //        Pause("");
            //        return true;    // Keep running the main menu
            //                        //case "4": // Create and show the sub-menu
            //                        //    SubMenu1 sm = new SubMenu1();
            //                        //    sm.Run();
            //                        //    return true;    // Keep running the main menu
            //}
            #endregion
            return true;
        }


        protected override void BeforeDisplayMenu()
        {
            PrintHeader();
        }


        private void PrintHeader()
        {
            SetColor(ConsoleColor.Cyan);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render($"    {"National Park"}"));
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Campsite Guide"));
            ResetColor();
        }
    }
}
