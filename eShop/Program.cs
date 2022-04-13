using System;
using System.Linq;
using Business.Services.Implementations;
using Data.Entities;

namespace eShop
{

    class Program
    {
        

        static void Main(string[] args)
        {
            var console = new eShopConsole();
            var showMenu = true;
            while (showMenu)
            {
                showMenu = console.MainMenu();
            }
        }  
    }
}
