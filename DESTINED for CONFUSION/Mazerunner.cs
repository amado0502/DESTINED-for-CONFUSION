using System;
using System.Data;
using System.Dynamic;
using System.IO.MemoryMappedFiles;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Rendering;


//0 paredes
//1 player1
//2 player2
//4 Posicion de ganada
//5 paredes
//6 trampa1
//7 trampa2
//8 trampa3

public class MazeRunner
{

  public int tamaño = 101;
  public (int,int) posicionGanada {get;set;}
  
  public int[,] maze = new int[101,101];
  public int this[int fila, int columna]
    {
        get => maze[fila, columna];
        set => maze[fila, columna] = value;
    }
    public int random_2_tamaño_2() //metodo para generar random
    {
        Random random = new Random();
        int random1 = random.Next(2,tamaño-2);
        if(random1%2==0)
        {return random1;}
        else
        {if(random1-1 > 1)
        {return random1-1;}
        else if(random1+1 < tamaño-3)
        {return random1+1;}
        else
        {return random_2_tamaño_2();}}
    }
     
  public MazeRunner()
  {
    
    Create_Maze(tamaño);
    
  }
  void Create_Maze(int tamaño)
  {
    for(int i = 0; i < this.tamaño; i++)
    {
        for(int j = 0; j < this.tamaño; j++)
        {
            if(i==0 || j==0 || i==tamaño-1 || j==tamaño-1)
            {maze[i,j] = 5;} //visitada
            else if(i%2==1 || j%2==1)
            {maze[i,j] = 0;}//paredes
            else
            {maze[i,j] = 1;}//no visitada
        }
    }
    Generator(random_2_tamaño_2(),random_2_tamaño_2());
    maze[1,2] = 5;

 for(int i = 0; i < this.tamaño; i++)
    {
        for(int j = 0; j < this.tamaño; j++)
        {
            if(i==0 || j==0 || i==tamaño-1 || j==tamaño-1)
            {maze[i,j] = 0;} //visitada
        }
    }

    Random random = new Random();
    int contador=0;
    
    while(contador==0)
    {
      var random1 = random.Next(tamaño/2,tamaño-1);
      var random2 = random.Next(1,3);
    switch(random2)
    {
      case 1:
      if(maze[tamaño-3,random1]!=0)
      {
        maze[tamaño-2,random1]=5;
        maze[tamaño-1,random1]=4;
        posicionGanada=(tamaño-1,random1);
        contador++;
      }
      break;
      case 2:
      if(maze[random1,tamaño-3]!=0)
      {
        maze[random1,tamaño-2]=5;
        maze[random1,tamaño-1]=4;
        posicionGanada=(random1,tamaño-1);

        contador++;
      }
      break;
    }
    
    }
  }
  bool Generator(int fila,int columna)
  {
   
    maze[fila,columna]=5;
    if(maze[fila-2,columna] == 5 && maze[fila+2,columna] == 5 && maze[fila,columna-2] == 5 && maze[fila,columna+2] == 5) 
    {return true;}
   
    List<int> li = [1,2,3,4]; 
    while(li.Count > 0)
    {
          Random random = new Random();
          int index = random.Next(li.Count);
          int dr = li[index];
          li.Remove(dr);

          int nfila;
          int mfila;
          int ncolumna;
          int mcolumna;
          if(dr==Directions.ARRIBA)
          {nfila = fila-2;
           mfila = fila-1;
           ncolumna = columna;
           mcolumna = columna;}
          else if(dr==Directions.ABAJO)
          {nfila = fila+2;
           mfila = fila+1;
           ncolumna = columna;
           mcolumna = columna;}
          else if(dr==Directions.DERECHA)
          {nfila = fila;
           mfila = fila;
           ncolumna = columna+2;
           mcolumna = columna+1;}
          else if(dr==Directions.IZQUIERDA)
          {nfila = fila;
           mfila = fila;
           ncolumna = columna-2;
           mcolumna = columna-1;}
          else
          {nfila = fila;
           mfila = fila;
           ncolumna = columna;
           mcolumna = columna;} 

          if(maze[nfila,ncolumna] != 5)
          {
            maze[mfila,mcolumna] = 5;
            Generator(nfila,ncolumna);}  
    }
    return true;
}




}

public class Directions()
{
    public static int ARRIBA = 1;
    public static int ABAJO = 2;
    public static int IZQUIERDA = 3;
    public static int DERECHA = 4;   
}