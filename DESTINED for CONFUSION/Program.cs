using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Threading;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Rendering;
using System;
using System.Threading.Tasks;



public static class Program
    {
        public static void Main(string[] args)
    {  
      
      MazeRunner mapa1 = new MazeRunner();
      string nombre1 = "LIAM";
      string nombre2 = "FIIN";
      MusicaDeFondo.IniciarMusica();
      Thread.Sleep(2500);
      Console.Clear(); 
      GamePages.PrintGameWelcome();
      GamePages.PrintStorie();
      Character Player1 = GamePages.PrintSelectionPlayer(mapa1,nombre1);
      Player1.posicionActual=(2,2);
      Character Player2 = GamePages.PrintSelectionPlayer(mapa1, nombre2);
      Player2.posicionActual=(1,2);
      GamePages.PrintGameScreen(mapa1,Player1);
      Logica.Jugar(mapa1,Player1,Player2);
  




  
    }
    
    }