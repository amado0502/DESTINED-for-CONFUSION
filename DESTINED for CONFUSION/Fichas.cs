using System.Collections;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Spectre.Console;


// Base class
public abstract class Character
{
    public string name {get;set;}
    public int wayout {get;set;}
    public int valorencasilla {get;set;}
    public bool poderActivado {get; set;}
    public bool cooldownActivado {get; set;}
    public int cooldownInicial {get;}
    public int cooldown {get; set;}
    public int velocidadInicial {get; }
    public int velocidad {get; set;}
    public int rastro {get; set;}
    public (int, int) posicionInicial { get; set; }
    public (int, int) posicionGanada { get; set; }
    public (int, int) posicionActual { get; set; }
    protected MazeRunner Mapa;

    public Character(MazeRunner mapa, (int, int) posicionInicial, (int, int) posicionGanada)
    {
        Mapa = mapa;
        name = "";
        this.posicionInicial = posicionInicial;
        this.posicionGanada = posicionGanada;
        this.posicionActual = posicionInicial;
        cooldown = 6;
        velocidad = 10;
        velocidadInicial = 10;
        cooldownInicial = 6;
        rastro = 1;
        poderActivado = false;
        wayout = 4;
    }
    public virtual void Poder(Character Player,Character Enemy, MazeRunner mapa, int turno)
    {
        // Metodo Implementado Distintamente en cada subclase
    }
    public virtual void ReestablecerCooldown(int cooldown)
    {
        if(cooldown==0)
        {this.cooldown = cooldownInicial;
        cooldownActivado=false;}
    }
    public virtual void ReestablecerVelocidad(int velocidad)
    {
        if(velocidad<=0)
        {this.velocidad= velocidadInicial;}
    }

    public virtual void ReestablecerPoder(MazeRunner mapa, Character Player, Character Enemy)
    {}



}

// Subclasses
public class Inteligencia : Character
{      
    public Inteligencia(MazeRunner mapa, (int, int) posicionInicial, (int, int) posicionGanada) 
        : base(mapa, posicionInicial, posicionGanada)
    {
             
    }

    public override void Poder(Character Player,Character Enemy, MazeRunner mapa, int turno)
    {
        Logica.FindaWayOut(mapa,Player.posicionActual.Item1,Player.posicionActual.Item2,wayout);
        mapa[Player.posicionActual.Item1,Player.posicionActual.Item2] = Player.valorencasilla;
        mapa[Enemy.posicionActual.Item1,Enemy.posicionActual.Item2] = Enemy.valorencasilla;
        mapa[Player.posicionGanada.Item1,Player.posicionGanada.Item2] = 4;
        cooldownActivado = true;
        AnsiConsole.MarkupInterpolated($"[BlueViolet]Ha usado su poder y la linea dorada lo guiara a la salida durante este turno[/]");
        Thread.Sleep(1200);
        Console.ReadKey(true);

    }

    public override void ReestablecerPoder(MazeRunner mapa, Character Player, Character Enemy)
    {
        Logica.ClearFindMap(mapa,Player,Enemy);
    }
}
public class Fuerza : Character
{
    public Fuerza(MazeRunner mapa, (int, int) posicionInicial, (int, int) posicionGanada) 
        : base(mapa, posicionInicial, posicionGanada)
    {
              
    }

    public override void Poder(Character Player,Character Enemy, MazeRunner mapa, int turno)
    {
       Logica.BreakWall(mapa,Player,turno);
       cooldownActivado = true;
       AnsiConsole.MarkupInterpolated($"[BlueViolet]Ha usado su poder y los espacios en dorado le indican las paredes que puede romper[/]");
       Thread.Sleep(1200);
       Console.ReadKey(true);
    }
}

public class Rapidez : Character
{
    public Rapidez(MazeRunner mapa, (int, int) posicionInicial, (int, int) posicionGanada) 
        : base(mapa, posicionInicial, posicionGanada)
    {
        
    }

    public override void Poder(Character Player,Character Enemy, MazeRunner mapa, int turno)
    {
        Player.velocidad+=15;
        Player.cooldownActivado=true;
        AnsiConsole.MarkupInterpolated($"[BlueViolet]Ha usado su poder y ahora puedes moverte mas durante este turno[/]");
        Thread.Sleep(1200);
       Console.ReadKey(true);
    }
}


public class  Persuasión: Character
{
    public Persuasión(MazeRunner mapa, (int, int) posicionInicial, (int, int) posicionGanada) 
        : base(mapa, posicionInicial, posicionGanada)
    {
       
    }

    public override void Poder(Character Player,Character Enemy,MazeRunner mapa, int turno)
    {
       Player.velocidad+=Enemy.velocidadInicial; 
       Player.cooldownActivado=true;
       AnsiConsole.MarkupInterpolated($"[BlueViolet]Ha usado su poder y tu compañero te ha cedido su turno[/]");
       Thread.Sleep(1200);
       Console.ReadKey(true);
    }
}


public class  Astucia: Character
{
    public Astucia(MazeRunner mapa, (int, int) posicionInicial, (int, int) posicionGanada) 
        : base(mapa, posicionInicial, posicionGanada)
    {
    }

    public override void Poder(Character Player,Character Enemy,MazeRunner mapa, int turno)
    {
        Logica.MoveRandomly(mapa,Enemy,turno,Player);
        Player.velocidad+=Enemy.velocidad;
        Player.cooldownActivado=true;
        AnsiConsole.MarkupInterpolated($"[BlueViolet]Ha usado su poder y tu compañero se ha movido sin sentido permitiendote jugar mas[/]");
        Thread.Sleep(1200);
       Console.ReadKey(true);
    }
}


public class Agilidad : Character
{
    public Agilidad(MazeRunner mapa, (int, int) posicionInicial, (int, int) posicionGanada) 
        : base(mapa, posicionInicial, posicionGanada)
    {
              
    }

    public override void Poder(Character Player,Character Enemy, MazeRunner mapa, int turno)
    {
       Logica.JumpWall(mapa,Player,turno);
       cooldownActivado = true;
       AnsiConsole.MarkupInterpolated($"[BlueViolet]Ha usado su poder y los espacios en dorado le indican a donde puede moverse[/]");
       Thread.Sleep(1200);
       Console.ReadKey(true);
    }
}