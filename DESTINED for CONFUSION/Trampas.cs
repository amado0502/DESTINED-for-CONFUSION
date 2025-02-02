using System;
using System.Collections.Generic;
using Spectre.Console;

public static class Trampa1
{
    public static int trapvalue = 6;
    static int numberoftraps = 10;
    public static List<(int,int)> Directions = new List<(int, int)>();

    public static void ApplyTrap((int, int) casilla,MazeRunner mapa, Character Player,Character Enemy)
    {
            AnsiConsole.MarkupInterpolated($"[BlueViolet]El laberinto es traicionero, incluso para los más precavidos. Has caído en un hoyo\n lograrás salir de ahí[/]");
            Enemy.velocidad+=Player.velocidadInicial*3;
            mapa[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
            Logica.turno=Enemy.valorencasilla;
            Player.velocidad=0;
            Player.ReestablecerVelocidad(Player.velocidad);
            mapa[casilla.Item1,casilla.Item2]=5;
            if (Directions != null)
            {
              Directions.Remove(casilla);
            }
        Thread.Sleep(900);
        GamePages.PrintGameScreen(mapa,Enemy);}
       

    public static void SpreadTrap(MazeRunner mapa)
    {
        Random random = new Random();
        while(numberoftraps!=0)
        {
          int a = random.Next(0,mapa.tamaño-1);
          int b = random.Next(0,mapa.tamaño-1);
          if(mapa[a,b]==5)
          {mapa[a,b] = trapvalue;
          Directions.Add((a,b));
          numberoftraps--;}
        }
    }
}




public static class Trampa2
{
    public static int trapvalue = 7;
    static int numberoftraps = 10;
    public static List<(int,int)> Directions = new List<(int, int)>();

    public static void ApplyTrap((int, int) casilla,MazeRunner mapa,Character Player, Character Enemy)
    {
            mapa[casilla.Item1,casilla.Item2]=0;
            GamePages.PrintGameScreen(mapa,Player);
            AnsiConsole.MarkupInterpolated($"[BlueViolet]El laberinto se divierte con tus tropiezos.\nUn obstáculo ha bloqueado este camino.Debes encontrar otra forma de continuar o superar\nel obstaculo [/]");
            Thread.Sleep(900);
            var option = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("PARA QUITAR EL OBSTACULO NECESITAS TIEMPO(3 TURNOS), LO HARÁS?")
        .PageSize(10)
        .MoreChoicesText("[grey](Muevete arriba y abajo para seleccionar una opción)[/]")
        .AddChoices(new[] {
            "SI", "NO",
        })
        );

// Echo the fruit back to the terminal
switch(option)
{
    case "SI":
        mapa[casilla.Item1,casilla.Item2]=5;
        if (Directions != null)
            {
              Directions.Remove(casilla);
            }
        Enemy.velocidad+=Player.velocidadInicial*3;
        Logica.turno=Enemy.valorencasilla;
            Player.velocidad=0;
            Player.ReestablecerVelocidad(Player.velocidad);
        Console.Clear();
        GamePages.PrintGameScreen(mapa,Player);
        break;
    case "NO":
    if (Directions != null)
            {
              Directions.Remove(casilla);
            }
        break;
}
            
        Thread.Sleep(900);}

    public static void SpreadTrap(MazeRunner mapa)
    {
        Random random = new Random();
        while(numberoftraps!=0)
        {
          int a = random.Next(0,mapa.tamaño-1);
          int b = random.Next(0,mapa.tamaño-1);
          if(mapa[a,b]==5)
          {mapa[a,b] = trapvalue;
          Directions.Add((a,b));
          numberoftraps--;}
        }
    }

}


public static class Trampa3
{
    public static int trapvalue = 8;
    static int numberoftraps = 10;
    public static List<(int,int)> Directions = new List<(int, int)>();

    public static void ApplyTrap((int, int) casilla,MazeRunner mapa, Character Player,Character Enemy)
    {
       
            var option = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("Acabas de encontrarte con una puerta secreta, quieres saber a donde va?")
        .PageSize(10)
        .MoreChoicesText("[grey](Muevete arriba y abajo para seleccionar una opción)[/]")
        .AddChoices(new[] {
            "SI", "NO",
        })
        );

// Echo the fruit back to the terminal
switch(option)
{
    case "SI":
        mapa[casilla.Item1,casilla.Item2]=5;
         if (Directions != null)
            {
              Directions.Remove(casilla);
            }
        mapa[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
        Console.Clear();
        GamePages.PrintGameScreen(mapa,Player);
        Thread.Sleep(100);
        Random random= new Random();
        int a =random.Next(0,mapa.tamaño-1);
        int b =random.Next(0,mapa.tamaño-1);
        while(mapa[a,b]!=5)
        {
        a =random.Next(0,mapa.tamaño-1);
        b =random.Next(0,mapa.tamaño-1);
        }
        mapa[a,b]=Player.valorencasilla;
        Player.posicionActual=(a,b);
        Console.Clear();
        GamePages.PrintGameScreen(mapa,Player);
        break;
    case "NO":
        break;
}
           
        Thread.Sleep(900);}

    public static void SpreadTrap(MazeRunner mapa)
    {
        Random random = new Random();
        while(numberoftraps!=0)
        {
          int a = random.Next(0,mapa.tamaño-1);
          int b = random.Next(0,mapa.tamaño-1);
          if(mapa[a,b]==5)
          {mapa[a,b] = trapvalue;
          Directions.Add((a,b));
          numberoftraps--;}
        }
    }

}

