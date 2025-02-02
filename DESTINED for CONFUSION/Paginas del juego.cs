using System;
using System.Diagnostics;
using Spectre.Console;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

public static class GamePages
{
    public static void PrintGameWelcome()
    {
        var titley = new FigletText("BIENVENIDO A").LeftJustified().Color(Color.Aqua);
        var title = new FigletText("DESTINED for CONFUSION:\nUNA ODISEA SIN RUMBO").LeftJustified().Color(Color.Aqua);
        Console.Clear();    
        AnsiConsole.WriteLine();
        AnsiConsole.Write(titley.Centered());
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();
        // Imprimir el título centrado
        AnsiConsole.Write(title.Centered());
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();


        
var option = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("OPCIONES")
        .PageSize(10)
        .MoreChoicesText("[grey](Muevete arriba y abajo para seleccionar una opción)[/]")
        .AddChoices(new[] {
            "EMPEZAR", "SALIR",
        })
        );

// Echo the fruit back to the terminal
switch(option)
{
    case "EMPEZAR":
        var title1 = new FigletText("VAMOS A EMPEZAR").LeftJustified().Color(Color.Aqua);
        Thread.Sleep(5000);
        Console.Clear();
        AnsiConsole.WriteLine();
        // Imprimir el título centrado
        AnsiConsole.Write(title1.Centered());
        break;
    case "SALIR":
        var title2 = new FigletText("GRACIAS POR JUGAR").LeftJustified().Color(Color.Aqua);
        Console.Clear();
        AnsiConsole.WriteLine();
        // Imprimir el título centrado
        AnsiConsole.Write(title2.Centered());
        Environment.Exit(0);
        break;
}

        
    }

    
    

    public static void PrintStorie()
    {   Console.Clear();
        var title = new FigletText("DESTINED for CONFUSION").LeftJustified().Color(Color.Aqua);
        AnsiConsole.Write(title.Centered());
        string frase = "En el pequeño pueblo de Aethel, al pie de una montaña cubierta de bosques milenarios, vivían dos jóvenes amigos: Liam y Finn. Liam, "
        +"\nun muchacho curioso y soñador, siempre con la cabeza en los libros de leyendas antiguas. Finn, un joven ágil y aventurero, con una habilidad innata "
        +"\npara sortear cualquier obstáculo. Desde niños, habían compartido historias de héroes perdidos en laberintos misteriosos, hasta que un día, la leyenda"
        +"\nse hizo realidad." 
        +"\nUn viejo ermitaño, conocido por sus extrañas historias y conocimientos oscuros, les reveló la existencia de un laberinto escondido en las profundidades"
        +"\ndel bosque. Decía que el laberinto no era solo un conjunto de pasajes intrincados, sino una prueba de valor y amistad, y que aquellos que encontraran la" 
        +"\nsalida serían recompensados con un don único. Liam y Finn, impulsados por la curiosidad y el deseo de aventura, decidieron adentrarse en el laberinto.";
       foreach (char letra in frase)
      {
      AnsiConsole.Markup(letra + "");
      Thread.Sleep(60);
      }
      Thread.Sleep(100);
      Console.ReadKey(true);  
      }


    
    public static Character PrintSelectionPlayer(MazeRunner mapa1, string nombre)
    {
        List<string> options = new List<string>{"INTELIGENCIA","FUERZA","AGILIDAD","PERSUASIÓN","ASTUCIA","RAPIDEZ"};

        Console.Clear();
        var title = new FigletText("DESTINED for CONFUSION").LeftJustified().Color(Color.Aqua);
        AnsiConsole.WriteLine();
        // Imprimir el título centrado
        AnsiConsole.Write(title.Centered());


        
    var option = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("SELECCIONE LA CUALIDAD DESEADA PARA " +nombre+", LE SERVIRÁ DE AYUDA EN EL JUEGO")
        .PageSize(10)
        .MoreChoicesText("[grey](Muevete arriba y abajo para seleccionar una cualidad)[/]")
        .AddChoices(options)
        );

       switch(option)
    {
    case "INTELIGENCIA":
        var title1 = new FigletText("BUENA ELECCIÓN").LeftJustified().Color(Color.Aqua);
        Console.Clear();
        AnsiConsole.WriteLine();
        AnsiConsole.Write(title.Centered());
        AnsiConsole.WriteLine();
        var confirmar = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("LA INTELIGENCIA TE PERMITE DESCIFRAR LA LÓGICA DEL LABERINTO POR UN MOMENTO\nLO CUAL TE GUIARÁ A LA SALIDA\nSELECCIONARÁS ESTA CUALIDAD?")
        .PageSize(10)
        .MoreChoicesText("[grey](Muevete arriba y abajo para seleccionar una cualidad)[/]")
        .AddChoices(new[]{"SI", "NO"})
        );
        switch(confirmar)
        {
            case "SI":
            Console.Clear();
            AnsiConsole.WriteLine();
            // Imprimir el título centrado
            AnsiConsole.Write(title.Centered());
            AnsiConsole.WriteLine();
            AnsiConsole.Write(title1.Centered());
            Thread.Sleep(1000);
            Inteligencia inteligencia = new Inteligencia(mapa1, (1, 2), mapa1.posicionGanada);
            return inteligencia;
            case "NO":
            return PrintSelectionPlayer(mapa1,nombre);

        }
        break;
    case "FUERZA":
        var title2 = new FigletText("BUENA ELECCIÓN").LeftJustified().Color(Color.Aqua);
        Console.Clear();
        AnsiConsole.WriteLine();
        AnsiConsole.Write(title.Centered());
        AnsiConsole.WriteLine();
        var confirmar1 = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("LA FUERZA TE PERMITE ABRIRTE PASO ROMPIÉNDO PAREDES\nSELECCIONARÁS ESTA CUALIDAD?")
        .PageSize(10)
        .MoreChoicesText("[grey](Muevete arriba y abajo para seleccionar una cualidad)[/]")
        .AddChoices(new[]{"SI", "NO"})
        );
        switch(confirmar1)
        {
            case "SI":
            Console.Clear();
            AnsiConsole.WriteLine();
             // Imprimir el título centrado
            AnsiConsole.Write(title.Centered());
            AnsiConsole.WriteLine();
            AnsiConsole.Write(title2.Centered());
            Thread.Sleep(1000);
            Fuerza fuerza = new Fuerza(mapa1, (1, 2),mapa1.posicionGanada);
            return fuerza;
            case "NO":
            return PrintSelectionPlayer(mapa1,nombre);

        }
        break; 

       
    case "AGILIDAD":
        var title3 = new FigletText("BUENA ELECCIÓN").LeftJustified().Color(Color.Aqua);
        Console.Clear();
        AnsiConsole.WriteLine();
        AnsiConsole.Write(title.Centered());
        AnsiConsole.WriteLine();

        var confirmar2 = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("LA AGILIDAD TE PERMITE SUPERAR OBSTÁCULOS Y PAREDES\nSELECCIONARÁS ESTA CUALIDAD?")
        .PageSize(10)
        .MoreChoicesText("[grey](Muevete arriba y abajo para seleccionar una cualidad)[/]")
        .AddChoices(new[]{"SI", "NO"})
        );
        switch(confirmar2)
        {
            case "SI":
            Console.Clear();
            AnsiConsole.WriteLine();
             // Imprimir el título centrado
            AnsiConsole.Write(title.Centered());
            AnsiConsole.WriteLine();
            AnsiConsole.Write(title3.Centered());
            Thread.Sleep(1000);
            Agilidad agilidad = new Agilidad(mapa1, (1, 2), mapa1.posicionGanada);
            return agilidad;
            case "NO":
            return PrintSelectionPlayer(mapa1,nombre);

        }
        break; 
        
    case "PERSUASIÓN":
        var title4 = new FigletText("BUENA ELECCIÓN").LeftJustified().Color(Color.Aqua);
        Console.Clear();
        AnsiConsole.WriteLine();
        AnsiConsole.Write(title.Centered());
        AnsiConsole.WriteLine();
        var confirmar3 = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("LA PERSUACIÓN TE PERMITE CONVENCER A TU AMIGO DE QUE TE CEDA SU TURNO\nSELECCIONARÁS ESTA CUALIDAD?")
        .PageSize(10)
        .MoreChoicesText("[grey](Muevete arriba y abajo para seleccionar una cualidad)[/]")
        .AddChoices(new[]{"SI", "NO"})
        );
        switch(confirmar3)
        {
            case "SI":
            Console.Clear();
            AnsiConsole.WriteLine();
            AnsiConsole.Write(title.Centered());
            AnsiConsole.WriteLine();
            // Imprimir el título centrado
            AnsiConsole.Write(title4.Centered());
            Thread.Sleep(1000);
            Persuasión persuasión = new Persuasión(mapa1, (1, 2), mapa1.posicionGanada);
            return persuasión;
            case "NO":
            return PrintSelectionPlayer(mapa1,nombre);
        }
        break;
    case "ASTUCIA":
        var title5 = new FigletText("BUENA ELECCIÓN").LeftJustified().Color(Color.Aqua);
        Console.Clear();
        AnsiConsole.WriteLine();
        AnsiConsole.Write(title.Centered());
        AnsiConsole.WriteLine();
        var confirmar4 = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("LA ASTUCIA TE PERMITE JUGAR UN TURNO ADICIONAL MIENTRAS QUE TU COMPAÑERO,\nPIERDE SU TURNO MOVIENDOSE ALEATORIAMENTE\nSELECCIONARÁS ESTA CUALIDAD?")
        .PageSize(10)
        .MoreChoicesText("[grey](Muevete arriba y abajo para seleccionar una cualidad)[/]")
        .AddChoices(new[]{"SI", "NO"})
        );
        switch(confirmar4)
        {
            case "SI":
            Console.Clear();
            AnsiConsole.WriteLine();
             AnsiConsole.Write(title.Centered());
         AnsiConsole.WriteLine();
        // Imprimir el título centrado
        AnsiConsole.Write(title5.Centered());
        Thread.Sleep(1000);
        Astucia astucia = new Astucia(mapa1, (1, 2), mapa1.posicionGanada);
        return astucia;
            case "NO":
            return PrintSelectionPlayer(mapa1,nombre);
        }
        break;
         
    case "RAPIDEZ" :
    var title6 = new FigletText("BUENA ELECCIÓN").LeftJustified().Color(Color.Aqua);
        Console.Clear();
        AnsiConsole.WriteLine();
        AnsiConsole.Write(title.Centered());
        AnsiConsole.WriteLine();
        var confirmar5 = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("LA RAPIDEZ TE PERMITE AVANZAR MÁS DURANTE UN TURNO\nSELECCIONARÁS ESTA CUALIDAD?")
        .PageSize(10)
        .MoreChoicesText("[grey](Muevete arriba y abajo para seleccionar una cualidad)[/]")
        .AddChoices(new[]{"SI", "NO"})
        );
        switch(confirmar5)
        {
            case "SI":
            Console.Clear();
            AnsiConsole.WriteLine();
            AnsiConsole.Write(title.Centered());
            AnsiConsole.WriteLine();
            // Imprimir el título centrado
            AnsiConsole.Write(title6.Centered());
            Thread.Sleep(1000);
            Rapidez rapidez = new Rapidez(mapa1, (1, 2), mapa1.posicionGanada);
            return rapidez;   
            case "NO":
            return PrintSelectionPlayer(mapa1,nombre);
        }
        break;
           
    }

    return PrintSelectionPlayer(mapa1,nombre); 
    }


    public static void PrintGameScreen(MazeRunner mapa1, Character Player) 
       {
        
        var title = new FigletText("DESTINED for CONFUSION").LeftJustified().Color(Color.Aqua);
       int x = Math.Max(0, Math.Min(Player.posicionActual.Item1 - 13, mapa1.tamaño - 31));
       int x1 = Math.Min(mapa1.tamaño, x + 25);
       int y = Math.Max(0, Math.Min(Player.posicionActual.Item2 - 13, mapa1.tamaño - 31));
       int y1 = Math.Min(mapa1.tamaño, y + 25);
        int z = 0;
        int z1 = 0;
        var canvas = new Canvas(25,25);
        int[,] matriz= new int[25,25];
        
           for (int i = x ; i < x1  ; i++,z++)
        {  
            for (int j = y; j < y1 ; j++,z1++)
            {  
               matriz[z,z1]=mapa1[i,j];

            }
            z1=0;
        }

        for (int k = 0; k < 25  ; k++)
        {  
            for (int l = 0; l < 25 ; l++)
            {  
               Color color = GetColorForValue(matriz[k, l]);
               canvas.SetPixel(l,k, color);
            }
        }

       
        //Centramos el canvas
        var table = new Table()
            .Centered() // Esto centra la tabla
            .Border(TableBorder.None) // Sin bordes
            .AddColumn(new TableColumn("").Centered()) // Columna centrada
            .AddRow(canvas); // Añadimos el canvas como contenido
       
            var TurnoTable = new Table();
        if(Player.name=="LIAM"){
            TurnoTable
            .Centered()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Aqua)
            .AddColumn(new TableColumn("[Aqua]ES EL TURNO DE LIAM[/]").Centered());
            TurnoTable.AddColumn(new TableColumn("[Aqua]OPCIONES [/] (ESC)").RightAligned());
            
            } 
         else
         {
            TurnoTable
            .Centered()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.DarkMagenta)
            .AddColumn(new TableColumn("[DarkMagenta]ES EL TURNO DE FIIN[/]").Centered());
            TurnoTable.AddColumn(new TableColumn("[DarkMagenta]OPCIONES [/] (ESC)").RightAligned());
            title.Color(Color.DarkMagenta);
         }   
        

        Console.Clear();
        //Imprimimos el turno de quien es
        AnsiConsole.WriteLine();
         AnsiConsole.Write(title.Centered());
        AnsiConsole.Write(TurnoTable);
        //Imprimimos la tabla de opciones
        // Imprimimos la tabla centrada con el canvas
        AnsiConsole.Write(table);
       }

        private static Color GetColorForValue(int value)
    {
        return value == 0 ? Color.White :  value == 20 ? Color.Gold1  : value == 1 ? Color.Aqua : value == 2 ? Color.DarkMagenta : Color.Black;
    }


    public static void PrintOptions(MazeRunner mapa1, Character Player)
    {
        var title = new FigletText("DESTINED for CONFUSION").LeftJustified().Color(Color.Aqua);

        Console.Clear();
         AnsiConsole.WriteLine();
         AnsiConsole.Write(title.Centered());
        var titley = new FigletText("OPCIONES").LeftJustified().Color(Color.CadetBlue_1);
        AnsiConsole.Write(titley);
        var option = AnsiConsole.Prompt<string>(
    new SelectionPrompt<string>()
        .Title("")
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to select an option)[/]")
        .AddChoices(new[] {
              "CONTINUAR","REINICIAR" ,"CONTROLES", "MUSICA","SALIR"
        })
        );

        switch(option)
        {
           case "REINICIAR":
           //falta agregar spinner y un thread.sleep
           Program.Main(new string[0]);
           break;
           case "SALIR":
           var title2 = new FigletText("GRACIAS POR JUGAR").LeftJustified().Color(Color.Aqua);
           Console.Clear();
           AnsiConsole.WriteLine();
           // Imprimir el título centrado
           AnsiConsole.Write(title2.Centered());
           Environment.Exit(0);
           break;
           case "CONTINUAR": 
           PrintGameScreen(mapa1,Player); 
           break;
           case "MUSICA":
           var musica = AnsiConsole.Prompt<string>(
           new SelectionPrompt<string>()
           .Title("MUSICA")
           .PageSize(10)
           .MoreChoicesText("[grey](Move up and down to select an option)[/]")
           .AddChoices(new[] {
            "SI", "NO"
        })
        );
        switch(musica)
        {
            case "SI":
            MusicaDeFondo.IniciarMusica();
            PrintGameScreen(mapa1,Player);
            break;
            case "NO":
            MusicaDeFondo.DetenerMusica();
            PrintGameScreen(mapa1,Player);
            break;
        }
           break;
           case "CONTROLES":
           var instructionsTable = new Table()
           .BorderColor(Color.Turquoise4)
           .Border(TableBorder.Ascii);
           instructionsTable.AddColumn("CONTROLES");
           instructionsTable.AddRow("Player1:\n'W' Moverse Arriba\n 'S' Moverse Abajo \n 'A' Moverse Izquierda \n 'D' Moverse Derecha \n 'V' Usar Poder");
           instructionsTable.AddRow("Player2:\n'UpArrow' Moverse Arriba\n 'DownArrow' Moverse Abajo \n 'LeftArrow' Moverse Izquierda \n 'RightArrow' Moverse Derecha \n 'L' Usar Poder");
           Console.Clear();
           Console.WriteLine();
           AnsiConsole.Write(instructionsTable.Centered());
           Console.ReadKey(true);
           PrintGameScreen(mapa1,Player);
           break;
           

        }


    }


    


   


}


