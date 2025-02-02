        using System.ComponentModel.DataAnnotations.Schema;
        using Spectre.Console;
        using Spectre.Console.Cli;
        using Spectre.Console.Rendering;
        using Spectre.Console.Extensions;
        using Spectre.Console.Advanced;
        using System.Threading.Tasks;
public static class Logica 
{
    static bool check = false;
    static Random random = new Random();
    static int random1;
    static int random2;
    static int random3;
    static int random4;
    static int random5;
    static int random6;
    public static int turno {get; set;}
    public static bool PosicionarJugadores(MazeRunner mapa, Character Player1, Character Player2)
    {
        random1 = random.Next(2,mapa.tamaño-2);
        random2 = random.Next(2,mapa.tamaño-2);
        random3 = random.Next(2,mapa.tamaño-2);
        random4 = random.Next(2,mapa.tamaño-2);
        random5 = random.Next(2,mapa.tamaño-2);
        random6 = random.Next(2,mapa.tamaño-2);
        if((random1!=random3) && (random2!=random4) && 
        (random1-random5>=mapa.tamaño/2 || random1-random5<=mapa.tamaño/2 || random2-random6<=mapa.tamaño/2 
        || random2-random6<=mapa.tamaño/2) && (random3-random5>=mapa.tamaño/2 || random3-random5<=mapa.tamaño/2 || random2-random6<=mapa.tamaño/2 
        || random4-random6<=mapa.tamaño/2) && mapa[random1,random2] ==5 && mapa[random3,random4] == 5
        && mapa[random5,random6] == 5)
        {
        Player1.valorencasilla = 1;
        Player2.valorencasilla = 2; 
        Player1.posicionActual=(random1,random2); 
        mapa[random5,random6]=9; 
        Player2.posicionActual=(random3,random4); 
        mapa[random1,random2]=1;
        mapa[random3,random4]=2;
        return true;
        }
        else return PosicionarJugadores(mapa,Player1,Player2);
    }

    public static void Jugar(MazeRunner mapa1,Character Player1, Character Player2)
    {  
        Player1.valorencasilla =1;
        Player1.name = "LIAM";
        Player2.valorencasilla =2;
        Player2.name = "FIIN";
        mapa1[Player1.posicionActual.Item1,Player1.posicionActual.Item2]=1;
        mapa1[Player2.posicionActual.Item1,Player2.posicionActual.Item2]=2;
        turno = 1;
        Trampa1.SpreadTrap(mapa1);
        Trampa2.SpreadTrap(mapa1);
        Trampa3.SpreadTrap(mapa1);
        Console.Clear();
        GamePages.PrintGameScreen(mapa1,Player1);
        while(Player1.posicionActual != Player1.posicionGanada ||  Player2.posicionActual != Player2.posicionGanada)
        {
           if(Player1.posicionActual==Player1.posicionGanada)
           {mapa1[Player1.posicionActual.Item1,Player1.posicionActual.Item2]=4;
            turno=2;}
           if(Player2.posicionActual==Player2.posicionGanada)
           {mapa1[Player2.posicionActual.Item1,Player2.posicionActual.Item2]=4;
            turno=1;}

           
             switch(turno)
        {
            case 1 :
                if(Player1.velocidad<=0)
                {turno=2;

                if(Player1.cooldownActivado==true)
                {
                Player1.cooldown--;
                Player1.ReestablecerCooldown(Player1.cooldown);
                }
                if(Player2.cooldownActivado==true)
                {
                Player2.cooldown--;
                Player2.ReestablecerCooldown(Player2.cooldown);
                }
                Player1.ReestablecerVelocidad(Player1.velocidad);
                Player1.ReestablecerPoder(mapa1,Player1,Player2);
                Thread.Sleep(1000);
                GamePages.PrintGameScreen(mapa1,Player2);
                break;}
                
                PlayerAction(mapa1,Player1,Player2,turno);
            break;
            case 2 :
                if(Player2.velocidad<=0)
                {turno=1;
                if(Player1.cooldownActivado==true)
                {
                Player1.cooldown--;
                Player1.ReestablecerCooldown(Player1.cooldown);
                }
                if(Player2.cooldownActivado==true)
                {
                Player2.cooldown--;
                Player2.ReestablecerCooldown(Player2.cooldown);
                }
                Player2.ReestablecerVelocidad(Player2.velocidad);
                Player2.ReestablecerPoder(mapa1,Player1,Player2);
                Thread.Sleep(1000);
                GamePages.PrintGameScreen(mapa1,Player1);
                break;}
                
                PlayerAction(mapa1,Player2,Player1,turno);
            break;
        }
        }
        Console.Clear();
         var title1 = new FigletText("Felicitaciones han logrado salir del laberinto").LeftJustified().Color(Color.Aqua);
         AnsiConsole.Write(title1.Centered());
    }


    public static void PlayerAction(MazeRunner mapa1,Character Player, Character Enemy,int turno)
    {
        switch(turno)
        {
        case 1:
            ConsoleKeyInfo jugada;
            jugada = Console.ReadKey(true);
            switch(jugada.Key)
            {   
            case ConsoleKey.W://arriba player1
                switch(mapa1[Player.posicionActual.Item1-1,Player.posicionActual.Item2])
                {
                    case 0:
                    break;
                    case 4:
                    case 20:
                    case 5 :
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1-1,Player.posicionActual.Item2);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 1;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player);
                        Player.velocidad--;
                    break;
                    case 2:
                        if(mapa1[Player.posicionActual.Item1-2,Player.posicionActual.Item2]==5 || mapa1[Player.posicionActual.Item1-2,Player.posicionActual.Item2]==20 || mapa1[Player.posicionActual.Item1-2,Player.posicionActual.Item2] == 4 )
                       { mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1-2,Player.posicionActual.Item2);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 1;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player);
                        Player.velocidad--;}
                    break;
                    case 6:
                    Trampa1.ApplyTrap((Player.posicionActual.Item1-1,Player.posicionActual.Item2),mapa1,Player,Enemy);
                    break;
                    case 7:
                    Trampa2.ApplyTrap((Player.posicionActual.Item1-1,Player.posicionActual.Item2),mapa1,Player,Enemy);
                    break;
                    case 8:
                    Trampa3.ApplyTrap((Player.posicionActual.Item1-1,Player.posicionActual.Item2),mapa1,Player,Enemy);
                    break;
                    
                }
                
            break;
            case ConsoleKey.S ://abajo player1
                switch(mapa1[Player.posicionActual.Item1+1,Player.posicionActual.Item2])
                {
                    case 0:
                    break;
                    case 4:
                    case 20:
                    case 5 :
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1+1,Player.posicionActual.Item2);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 2;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player); 
                        Player.velocidad--;
                    break;
                    case 2:
                        if(mapa1[Player.posicionActual.Item1+2,Player.posicionActual.Item2]==5 || mapa1[Player.posicionActual.Item1+2,Player.posicionActual.Item2]==20 || mapa1[Player.posicionActual.Item1+2,Player.posicionActual.Item2] == 4 )
                        {mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1+2,Player.posicionActual.Item2);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 2;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player); 
                        Player.velocidad--;}
                    break;
                    case 6:
                    Trampa1.ApplyTrap((Player.posicionActual.Item1+1,Player.posicionActual.Item2),mapa1,Player,Enemy);
                    break;
                    case 7:
                    Trampa2.ApplyTrap((Player.posicionActual.Item1+1,Player.posicionActual.Item2),mapa1,Player,Enemy);
                    break;
                    case 8:
                    Trampa3.ApplyTrap((Player.posicionActual.Item1+1,Player.posicionActual.Item2),mapa1,Player,Enemy);
                    break;
                    
                }
            break;
            case ConsoleKey.A ://izquierda player1
                switch(mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2-1])
                {
                    case 0:
                    break;
                    case 4:
                    case 20:
                    case 5 :
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1,Player.posicionActual.Item2-1);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 3;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player); 
                        Player.velocidad--;
                    break;
                    case 2:
                        if(mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2-2]==5 || mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2-2]==20 || mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2-2] == 4 )
                        {mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1,Player.posicionActual.Item2-2);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 3;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player); 
                        Player.velocidad--;}
                    break;
                    case 6:
                    Trampa1.ApplyTrap((Player.posicionActual.Item1,Player.posicionActual.Item2-1),mapa1,Player,Enemy);
                    break;
                    case 7:
                    Trampa2.ApplyTrap((Player.posicionActual.Item1,Player.posicionActual.Item2-1),mapa1,Player,Enemy);
                    break;
                    case 8:
                    Trampa3.ApplyTrap((Player.posicionActual.Item1,Player.posicionActual.Item2-1),mapa1,Player,Enemy);
                    break;
                   
                }
            break;
            case ConsoleKey.D ://derecha player1
                switch(mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2+1])
                {
                    case 0:
                    break;
                    case 4:
                    case 20:
                    case 5:
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1,Player.posicionActual.Item2+1);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 4;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player);
                        Player.velocidad--;
                    break;
                    case 2:
                        if(mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2+2]==5 || mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2+2]==20 || mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2+2] == 4 )
                        {mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1,Player.posicionActual.Item2+2);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 4;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player);
                        Player.velocidad--;}
                    break;
                    case 6:
                    Trampa1.ApplyTrap((Player.posicionActual.Item1,Player.posicionActual.Item2+1),mapa1,Player,Enemy);
                    break;
                    case 7:
                    Trampa2.ApplyTrap((Player.posicionActual.Item1,Player.posicionActual.Item2+1),mapa1,Player,Enemy);
                    break;
                    case 8:
                    Trampa3.ApplyTrap((Player.posicionActual.Item1,Player.posicionActual.Item2+1),mapa1,Player,Enemy);
                    break;
                    
                }
                
            break;
            case ConsoleKey.V ://poder player1
                if(Player.cooldownActivado)
                {
                AnsiConsole.Markup($"[BlueViolet]LO SENTIMOS NO PUEDE USAR SU PODER AHORA[/]");
                break;
                }
                Player.Poder(Player,Enemy,mapa1,turno);
                Console.Clear();
                GamePages.PrintGameScreen(mapa1,Player);
                Player.velocidad--;
            break;
            case ConsoleKey.Escape :
                GamePages.PrintOptions(mapa1,Player);
            break;
            }
        break;
        case 2:
        ConsoleKeyInfo jugada1;
            jugada1 = Console.ReadKey(true);
            switch(jugada1.Key)
            {   
            case ConsoleKey.UpArrow://arriba player2
                switch(mapa1[Player.posicionActual.Item1-1,Player.posicionActual.Item2])
                {
                    case 0:
                    break;
                    case 4:
                    case 20:
                    case 5 :
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1-1,Player.posicionActual.Item2);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 1;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player);
                        Player.velocidad--;
                    break;
                    case 1:
                        if(mapa1[Player.posicionActual.Item1-2,Player.posicionActual.Item2]==5 || mapa1[Player.posicionActual.Item1-2,Player.posicionActual.Item2]==20 || mapa1[Player.posicionActual.Item1-2,Player.posicionActual.Item2] == 4 )
                        {mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1-2,Player.posicionActual.Item2);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 1;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player);
                        Player.velocidad--;}
                    break;
                    case 6:
                    Trampa1.ApplyTrap((Player.posicionActual.Item1-1,Player.posicionActual.Item2),mapa1,Player,Enemy);
                    break;
                    case 7:
                    Trampa2.ApplyTrap((Player.posicionActual.Item1-1,Player.posicionActual.Item2),mapa1,Player,Enemy);
                    break;
                    case 8:
                    Trampa3.ApplyTrap((Player.posicionActual.Item1-1,Player.posicionActual.Item2),mapa1,Player,Enemy);
                    break;
                    
                }
                
            break;
            case ConsoleKey.DownArrow ://abajo player2
                switch(mapa1[Player.posicionActual.Item1+1,Player.posicionActual.Item2])
                {
                    case 0:
                    break;
                    case 4:
                    case 20 :
                    case 5 :
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1+1,Player.posicionActual.Item2);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 2;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player); 
                        Player.velocidad--;
                    break;
                    case 1:
                        if(mapa1[Player.posicionActual.Item1+2,Player.posicionActual.Item2]==5 || mapa1[Player.posicionActual.Item1+2,Player.posicionActual.Item2]==20 || mapa1[Player.posicionActual.Item1+2,Player.posicionActual.Item2] == 4 )
                        {mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1+2,Player.posicionActual.Item2);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 2;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player); 
                        Player.velocidad--;}
                    break;
                    case 6:
                    Trampa1.ApplyTrap((Player.posicionActual.Item1+1,Player.posicionActual.Item2),mapa1,Player,Enemy);
                    break;
                    case 7:
                    Trampa2.ApplyTrap((Player.posicionActual.Item1+1,Player.posicionActual.Item2),mapa1,Player,Enemy);
                    break;
                    case 8:
                    Trampa3.ApplyTrap((Player.posicionActual.Item1+1,Player.posicionActual.Item2),mapa1,Player,Enemy);
                    break;
                    
                }
            break;
            case ConsoleKey.LeftArrow ://izquierda player2
                switch(mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2-1])
                {
                    case 0:
                    break;
                    case 4:
                    case 20:
                    case 5 :
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1,Player.posicionActual.Item2-1);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 3;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player); 
                        Player.velocidad--;
                    break;
                    case 1:
                        if(mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2-2]==5 || mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2-2]==20 || mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2-2] == 4 )
                        {mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1,Player.posicionActual.Item2-2);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 3;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player); 
                        Player.velocidad--;}
                    break;
                    case 6:
                    Trampa1.ApplyTrap((Player.posicionActual.Item1,Player.posicionActual.Item2-1),mapa1,Player,Enemy);
                    break;
                    case 7:
                    Trampa2.ApplyTrap((Player.posicionActual.Item1,Player.posicionActual.Item2-1),mapa1,Player,Enemy);
                    break;
                    case 8:
                    Trampa3.ApplyTrap((Player.posicionActual.Item1,Player.posicionActual.Item2-1),mapa1,Player,Enemy);
                    break;
                    
                }
            break;
            case ConsoleKey.RightArrow ://derecha player2
                switch(mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2+1])
                {
                    case 0:
                    break;
                    case 4:
                    case 20:
                    case 5 :
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1,Player.posicionActual.Item2+1);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 4;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player);
                        Player.velocidad--;
                    break;
                    case 1:
                        if(mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2+2]==5 || mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2+2]==20 || mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2+2] == 4 )
                        {mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1,Player.posicionActual.Item2+2);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 4;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player);
                        Player.velocidad--;}
                    break;
                    case 6:
                    Trampa1.ApplyTrap((Player.posicionActual.Item1,Player.posicionActual.Item2+1),mapa1,Player,Enemy);
                    break;
                    case 7:
                    Trampa2.ApplyTrap((Player.posicionActual.Item1,Player.posicionActual.Item2+1),mapa1,Player,Enemy);
                    break;
                    case 8:
                    Trampa3.ApplyTrap((Player.posicionActual.Item1,Player.posicionActual.Item2+1),mapa1,Player,Enemy);
                    break;
                    
                }
                
            break;
            case ConsoleKey.L ://poder player2
                if(Player.cooldownActivado)
                {
                AnsiConsole.Markup($"[BlueViolet]LO SENTIMOS NO PUEDE USAR SU PODER AHORA");
                break;
                }
                Player.Poder(Player,Enemy,mapa1,turno);
                Console.Clear();
                GamePages.PrintGameScreen(mapa1,Player);
                Player.velocidad--;
            break;
            case ConsoleKey.Escape :
                GamePages.PrintOptions(mapa1,Player);
            break;
            }
        break;
        }


    }

    
    public static bool FindaWayOut(MazeRunner mapa, int fila, int columna, int wayout)
    {
        if(check)
        {return true;}

        mapa[fila,columna] = 20;

        if(mapa[fila,columna] == wayout) 
        {return true;}

        if((mapa[fila-1,columna] == 20 || mapa[fila-1,columna] == 0) && (mapa[fila+1,columna] == 20 || mapa[fila+1,columna] == 0) 
        && (mapa[fila,columna-1] == 20 || mapa[fila,columna-1] == 0) && (mapa[fila,columna+1] == 20 || mapa[fila,columna+1] == 0)) 
        {   mapa[fila,columna] = 5;
        return true;}

        List<int> li = [1,2,3,4];

        while(li.Count > 0)
        {     
            if(check)
            {return true;}
            Random random = new Random();
            int index = random.Next(li.Count);
            int dr = li[index];
            li.Remove(dr);

            int mfila;
            int mcolumna;
            if(dr==Directions.ARRIBA)
            {mfila = fila-1;
            mcolumna = columna;}
            else if(dr==Directions.ABAJO)
            {mfila = fila+1;
            mcolumna = columna;}
            else if(dr==Directions.DERECHA)
            {mfila = fila;
            mcolumna = columna+1;}
            else if(dr==Directions.IZQUIERDA)
            {mfila = fila;
            mcolumna = columna-1;}
            else
            {mfila = fila;
            mcolumna = columna;} 

            if(mapa[mfila,mcolumna] == wayout)
            { check = true;
              return true; }
            else if(mapa[mfila,mcolumna] != 0 && mapa[mfila,mcolumna] != 20)
            {FindaWayOut(mapa,mfila,mcolumna,4);}  
        }
        if(check)
        {return true;}
        mapa[fila,columna] = 5;
        return true;
        }  


    public static void ClearFindMap(MazeRunner mapa, Character Player1, Character Player2)
    {
        check = false;
        for(int i = 0; i < mapa.tamaño; i++)
        {
            for(int j = 0; j < mapa.tamaño; j++)
            {
                if(mapa[i,j]==20)
                {mapa[i,j] = 5;}
            }
        }
        mapa[Player1.posicionActual.Item1,Player1.posicionActual.Item2]=Player1.valorencasilla;
        mapa[Player2.posicionActual.Item1,Player2.posicionActual.Item2]=Player2.valorencasilla;
        foreach((int,int) a in Trampa1.Directions)
        {
            mapa[a.Item1,a.Item2]=Trampa1.trapvalue;
        }
        foreach((int,int) a in Trampa2.Directions)
        {
            mapa[a.Item1,a.Item2]=Trampa2.trapvalue;
        }
        foreach((int,int) a in Trampa3.Directions)
        {
            mapa[a.Item1,a.Item2]=Trampa3.trapvalue;
        }
    }

    public static void JumpWall(MazeRunner mapa,Character Player, int turno)
    {
        int fila = Player.posicionActual.Item1;
        int columna = Player.posicionActual.Item2;
        if(mapa[fila-1,columna]!=5 && mapa[fila-2,columna]==5)
        {mapa[fila-2,columna]=20;}
        if(mapa[fila+1,columna]!=5 && mapa[fila+2,columna]==5)
        {mapa[fila+2,columna]=20;}
        if(mapa[fila,columna-1]!=5 && mapa[fila,columna-2]==5)
        {mapa[fila,columna-2]=20;}
        if(mapa[fila,columna+1]!=5 && mapa[fila,columna+2]==5)
        {mapa[fila,columna+2]=20;}

        Console.Clear();
        GamePages.PrintGameScreen(mapa,Player);

        ConsoleKeyInfo jugada;
        jugada = Console.ReadKey(true);


        switch(turno)
        {
            case 1:
                if(jugada.Key == ConsoleKey.W && mapa[fila-2,columna]==20)
                {Player.posicionActual = (fila-2,columna);
                mapa[fila,columna] = 5;
                mapa[fila-2,columna]=Player.valorencasilla;
                if(mapa[fila+2,columna]==20)
                mapa[fila+2,columna]=5;
                if(mapa[fila,columna-2]==20)
                mapa[fila,columna-2]=5;
                if(mapa[fila,columna+2]==20)
                mapa[fila,columna+2]=5;
                }
                else if(jugada.Key == ConsoleKey.S && mapa[fila+2,columna]==20)
                {Player.posicionActual = (fila+2,columna);
                mapa[fila,columna] = 5;
                if(mapa[fila-2,columna]==20)
                mapa[fila-2,columna]=5;
                mapa[fila+2,columna]=Player.valorencasilla;
                if(mapa[fila,columna-2]==20)
                mapa[fila,columna-2]=5;
                if(mapa[fila,columna+2]==20)
                mapa[fila,columna+2]=5;}
                else if(jugada.Key == ConsoleKey.A && mapa[fila,columna-2]==20)
                {Player.posicionActual = (fila,columna-2);
                mapa[fila,columna] = 5;
                if(mapa[fila-2,columna]==20)
                mapa[fila-2,columna]=5;
                if(mapa[fila+2,columna]==20)
                mapa[fila+2,columna]=5;
                mapa[fila,columna-2]=Player.valorencasilla;
                if(mapa[fila,columna+2]==20)
                mapa[fila,columna+2]=5;}
                else if(jugada.Key == ConsoleKey.D &&  mapa[fila,columna+2]==20)
                {Player.posicionActual = (fila,columna+2);
                mapa[fila,columna] = 5;
                if(mapa[fila-2,columna]==20)
                mapa[fila-2,columna]=5;
                if(mapa[fila+2,columna]==20)
                mapa[fila+2,columna]=5;
                if(mapa[fila,columna-2]==20)
                mapa[fila,columna-2]=5;
                mapa[fila,columna+2]=Player.valorencasilla;}
                else if(jugada.Key == ConsoleKey.Escape)
                {GamePages.PrintOptions(mapa,Player);
                JumpWall(mapa,Player,turno);}
                else
                {JumpWall(mapa,Player,turno);}
            break;
            case 2:
                if(jugada.Key == ConsoleKey.UpArrow && mapa[fila-2,columna]==20)
                {Player.posicionActual = (fila-2,columna);
                mapa[fila,columna] = 5;
                mapa[fila-2,columna]=Player.valorencasilla;
                if(mapa[fila+2,columna]==20)
                mapa[fila+2,columna]=5;
                if(mapa[fila,columna-2]==20)
                mapa[fila,columna-2]=5;
                if(mapa[fila,columna+2]==20)
                mapa[fila,columna+2]=5;
                }
                else if(jugada.Key == ConsoleKey.DownArrow && mapa[fila+2,columna]==20)
                {Player.posicionActual = (fila+2,columna);
                mapa[fila,columna] = 5;
                if(mapa[fila-2,columna]==20)
                mapa[fila-2,columna]=5;
                mapa[fila+2,columna]=Player.valorencasilla;
                if(mapa[fila,columna-2]==20)
                mapa[fila,columna-2]=5;
                if(mapa[fila,columna+2]==20)
                mapa[fila,columna+2]=5;}
                else if(jugada.Key == ConsoleKey.LeftArrow && mapa[fila,columna-2]==20)
                {Player.posicionActual = (fila,columna-2);
                mapa[fila,columna] = 5;
                mapa[fila-2,columna]=5;
                mapa[fila+2,columna]=5;
                mapa[fila,columna-2]=Player.valorencasilla;
                mapa[fila,columna+2]=5;}
                else if(jugada.Key == ConsoleKey.RightArrow &&  mapa[fila,columna+2]==20)
                {Player.posicionActual = (fila,columna+2);
                mapa[fila,columna] = 5;
                if(mapa[fila-2,columna]==20)
                mapa[fila-2,columna]=5;
                if(mapa[fila+2,columna]==20)
                mapa[fila+2,columna]=5;
                if(mapa[fila,columna-2]==20)
                mapa[fila,columna-2]=5;
                mapa[fila,columna+2]=Player.valorencasilla;}
                else if(jugada.Key == ConsoleKey.Escape)
                {GamePages.PrintOptions(mapa,Player);
                JumpWall(mapa,Player,turno);}
                else
                {JumpWall(mapa,Player,turno);}
            break;
        }

    }

    public static void BreakWall(MazeRunner mapa, Character Player,int turno)
    {
       int fila = Player.posicionActual.Item1;
        int columna = Player.posicionActual.Item2;
        if(mapa[fila-1,columna]==0 && mapa[fila-2,columna]!=0)
        {mapa[fila-1,columna]=20;}
        if(mapa[fila+1,columna]==0 && mapa[fila+2,columna]!=0)
        {mapa[fila+1,columna]=20;}
        if(mapa[fila,columna-1]==0 && mapa[fila,columna-2]!=0)
        {mapa[fila,columna-1]=20;}
        if(mapa[fila,columna+1]==0 && mapa[fila,columna+2]!=0)
        {mapa[fila,columna+1]=20;}

        Console.Clear();
        GamePages.PrintGameScreen(mapa,Player);

        ConsoleKeyInfo jugada;
        jugada = Console.ReadKey(true);


        switch(turno)
        {
            case 1:
                if(jugada.Key == ConsoleKey.W && mapa[fila-1,columna]==20)
                {
                mapa[fila-1,columna]=5;
                if(mapa[fila+1,columna]==20)
                mapa[fila+1,columna]=0;
                if(mapa[fila,columna-1]==20)
                mapa[fila,columna-1]=0;
                if(mapa[fila,columna+1]==20)
                mapa[fila,columna+1]=0;
                }
                else if(jugada.Key == ConsoleKey.S && mapa[fila+1,columna]==20)
                {
                if(mapa[fila-1,columna]==20)
                mapa[fila-1,columna]=0;
                mapa[fila+1,columna]=5;
                if(mapa[fila,columna-2]==20)
                mapa[fila,columna-1]=0;
                if(mapa[fila,columna+1]==20)
                mapa[fila,columna+1]=0;}
                else if(jugada.Key == ConsoleKey.A && mapa[fila,columna-1]==20)
                {
                if(mapa[fila-1,columna]==20)
                mapa[fila-1,columna]=0;
                if(mapa[fila+1,columna]==20)
                mapa[fila+1,columna]=0;
                mapa[fila,columna-1]=5;
                if(mapa[fila,columna+1]==20)
                mapa[fila,columna+1]=0;}
                else if(jugada.Key == ConsoleKey.D &&  mapa[fila,columna+1]==20)
                {
                if(mapa[fila-1,columna]==20)
                mapa[fila-1,columna]=0;
                if(mapa[fila+1,columna]==20)
                mapa[fila+1,columna]=0;
                if(mapa[fila,columna-1]==20)
                mapa[fila,columna-1]=0;
                mapa[fila,columna+1]=5;}
                else if(jugada.Key == ConsoleKey.Escape)
                {GamePages.PrintOptions(mapa,Player);
                BreakWall(mapa,Player,turno);}
                else
                {BreakWall(mapa,Player,turno);}
            break;
            case 2:
                if(jugada.Key == ConsoleKey.UpArrow && mapa[fila-1,columna]==20)
                {mapa[fila-1,columna]=5;
                if(mapa[fila+1,columna]==20)
                mapa[fila+1,columna]=0;
                if(mapa[fila,columna-1]==20)
                mapa[fila,columna-1]=0;
                if(mapa[fila,columna+1]==20)
                mapa[fila,columna+1]=0;
                }
                else if(jugada.Key == ConsoleKey.DownArrow && mapa[fila+1,columna]==20)
                {if(mapa[fila-1,columna]==20)
                mapa[fila-1,columna]=0;
                mapa[fila+1,columna]=5;
                if(mapa[fila,columna-1]==20)
                mapa[fila,columna-1]=0;
                if(mapa[fila,columna+1]==20)
                mapa[fila,columna+1]=0;}
                else if(jugada.Key == ConsoleKey.LeftArrow && mapa[fila,columna-1]==20)
                {if(mapa[fila-1,columna]==20)
                mapa[fila-1,columna]=0;
                if(mapa[fila+1,columna]==20)
                mapa[fila+1,columna]=0;
                mapa[fila,columna-1]=5;
                if(mapa[fila,columna+1]==20)
                mapa[fila,columna+1]=0;}
                else if(jugada.Key == ConsoleKey.RightArrow &&  mapa[fila,columna+1]==20)
                {
                    if(mapa[fila-1,columna]==20)
                mapa[fila-1,columna]=0;
                if(mapa[fila+1,columna]==20)
                mapa[fila+1,columna]=0;
                if(mapa[fila,columna-1]==20)
                mapa[fila,columna-1]=0;
                mapa[fila,columna+1]=5;
                }
                else if(jugada.Key == ConsoleKey.Escape)
                {GamePages.PrintOptions(mapa,Player);
                BreakWall(mapa,Player,turno);}
                else
                {BreakWall(mapa,Player,turno);}
            break;
        }
    }

    public static void MoveRandomly(MazeRunner mapa1, Character Player,int turno,Character Enemy)
    {
        List<ConsoleKey> li = [ConsoleKey.W,ConsoleKey.S,ConsoleKey.A,ConsoleKey.D];
        while(Player.velocidad>0)
        {
            Thread.Sleep(500);
            
            Random random = new Random();
            int index = random.Next(li.Count);
            ConsoleKey jugada = li[index];
            switch(jugada)
            {   
            case ConsoleKey.W://arriba player1
                switch(mapa1[Player.posicionActual.Item1-1,Player.posicionActual.Item2])
                {
                    case 0:
                    break;
                    case 4:
                    case 20:
                    case 5 :
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1-1,Player.posicionActual.Item2);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 1;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player);
                        Player.velocidad--;
                    break;
                    case 1:
                    case 2:
                        if(mapa1[Player.posicionActual.Item1-2,Player.posicionActual.Item2]==5 || mapa1[Player.posicionActual.Item1-2,Player.posicionActual.Item2]==20 || mapa1[Player.posicionActual.Item1-2,Player.posicionActual.Item2] == 4 )
                       { mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1-2,Player.posicionActual.Item2);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 1;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player);
                        Player.velocidad--;}
                    break;
                    case 6:
                    Trampa1.ApplyTrap((Player.posicionActual.Item1-1,Player.posicionActual.Item2),mapa1,Player,Enemy);
                    break;
                    case 7:
                    Trampa1.ApplyTrap((Player.posicionActual.Item1-1,Player.posicionActual.Item2),mapa1,Player,Enemy);
                    break;
                    case 8:
                    Trampa1.ApplyTrap((Player.posicionActual.Item1-1,Player.posicionActual.Item2),mapa1,Player,Enemy);
                    break;
                    
                }
                
            break;
            case ConsoleKey.S ://abajo player1
                switch(mapa1[Player.posicionActual.Item1+1,Player.posicionActual.Item2])
                {
                    case 0:
                    break;
                    case 4:
                    case 20:
                    case 5 :
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1+1,Player.posicionActual.Item2);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 2;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player); 
                        Player.velocidad--;
                    break;
                    case 1:
                    case 2:
                        if(mapa1[Player.posicionActual.Item1+2,Player.posicionActual.Item2]==5 || mapa1[Player.posicionActual.Item1+2,Player.posicionActual.Item2]==20 || mapa1[Player.posicionActual.Item1+2,Player.posicionActual.Item2] == 4 )
                        {mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1+2,Player.posicionActual.Item2);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 2;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player); 
                        Player.velocidad--;}
                    break;
                    case 6:
                    Trampa1.ApplyTrap((Player.posicionActual.Item1+1,Player.posicionActual.Item2),mapa1,Player,Enemy);
                    break;
                    case 7:
                    Trampa1.ApplyTrap((Player.posicionActual.Item1+1,Player.posicionActual.Item2),mapa1,Player,Enemy);
                    break;
                    case 8:
                    Trampa1.ApplyTrap((Player.posicionActual.Item1+1,Player.posicionActual.Item2),mapa1,Player,Enemy);
                    break;
                    
                }
            break;
            case ConsoleKey.A ://izquierda player1
                switch(mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2-1])
                {
                    case 0:
                    break;
                    case 4:
                    case 20:
                    case 5:
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1,Player.posicionActual.Item2-1);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 3;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player); 
                        Player.velocidad--;
                    break;
                    case 1:
                    case 2:
                        if(mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2-2]==5 || mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2-2]==20 || mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2-2] == 4 )
                        {mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1,Player.posicionActual.Item2-2);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 3;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player); 
                        Player.velocidad--;}
                    break;
                    case 6:
                    Trampa1.ApplyTrap((Player.posicionActual.Item1,Player.posicionActual.Item2-1),mapa1,Player,Enemy);
                    break;
                    case 7:
                    Trampa1.ApplyTrap((Player.posicionActual.Item1,Player.posicionActual.Item2-1),mapa1,Player,Enemy);
                    break;
                    case 8:
                    Trampa1.ApplyTrap((Player.posicionActual.Item1,Player.posicionActual.Item2-1),mapa1,Player,Enemy);
                    break;
                    
                }
            break;
            case ConsoleKey.D ://derecha player1
                switch(mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2+1])
                {
                    case 0:
                    break;
                    case 4:
                    case 20:
                    case 5:
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1,Player.posicionActual.Item2+1);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 4;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player);
                        Player.velocidad--;
                    break;
                    case 1:
                    case 2:
                        if(mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2+2]==5 || mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2+2]==20 || mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2+2] == 4 )
                        {mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=5;
                        Player.posicionActual = (Player.posicionActual.Item1,Player.posicionActual.Item2+2);
                        mapa1[Player.posicionActual.Item1,Player.posicionActual.Item2]=Player.valorencasilla;
                        Player.rastro = 4;
                        Console.Clear();
                        GamePages.PrintGameScreen(mapa1,Player);
                        Player.velocidad--;}
                        break;
                        case 6:
                        Trampa1.ApplyTrap((Player.posicionActual.Item1,Player.posicionActual.Item2+1),mapa1,Player,Enemy);
                        break;
                        case 7:
                        Trampa1.ApplyTrap((Player.posicionActual.Item1,Player.posicionActual.Item2+1),mapa1,Player,Enemy);
                        break;
                        case 8:
                        Trampa1.ApplyTrap((Player.posicionActual.Item1,Player.posicionActual.Item2+1),mapa1,Player,Enemy);
                        break;
                        
                }
                break;
                }

     }


   }
}

public class TeclasdeMovimiento()
{
    public static ConsoleKey ARRIBA = ConsoleKey.W;
    public static ConsoleKey ABAJO = ConsoleKey.S;
    public static ConsoleKey IZQUIERDA = ConsoleKey.A;
    public static ConsoleKey DERECHA = ConsoleKey.D;   
}