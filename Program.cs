using System;
using System.Collections.Generic;

class Llamada
{
    public string numeroOrigen;
    public string numeroDestino;
    public int duracion;
    private double  costePorSegundo = 0;

    public Llamada(string numeroOrigen, string numeroDestino, int duracion)
    {
        this.numeroOrigen = numeroOrigen;
        this.numeroDestino = numeroDestino;
        this.duracion = duracion;
    }

    public virtual  double Coste()
    {
        return duracion * costePorSegundo;
    }
}

class LlamadaLocal : Llamada
{
    private double costePorSegundo = 0.15;

    public LlamadaLocal(string numeroOrigen, string numeroDestino, int duracion)
        : base(numeroOrigen, numeroDestino, duracion)
    {
    }

    override public double Coste()
    {
        return duracion * costePorSegundo;
    }

    public string tostring ()
    {
        return Coste().ToString();
    }
}

class LlamadaProvincial : Llamada
{
    private int franjaHoraria;

    public LlamadaProvincial(string numeroOrigen, string numeroDestino, int duracion, int franjaHoraria)
        : base(numeroOrigen, numeroDestino, duracion)
    {
        this.franjaHoraria = franjaHoraria;
    }

    override public double Coste()
    {
        double  costePorSegundo = 0;

        switch (franjaHoraria)
        {
            case 1:
                costePorSegundo = 0.20;
                break;
            case 2:
                costePorSegundo = 0.25;
                break;
            case 3:
                costePorSegundo = 0.30;
                break;
        }

        return duracion * costePorSegundo;
    }
}

class Centralita
{
    private List<Llamada> llamadas = new List<Llamada>();
    private int numLlamadas = 0;
    private double facturacionTotal = 0;

    public void RegistrarLlamada(Llamada llamada)
    {
        llamadas.Add(llamada);
        numLlamadas++;
        facturacionTotal += llamada switch
        {
            LlamadaLocal local => local.Coste(),
            LlamadaProvincial provincial => provincial.Coste(),
            _ => 0,
        };
        Console.WriteLine("Llamada registrada: " + llamada.numeroOrigen + " -> " + llamada.numeroDestino + " (" + llamada.duracion + " segundos)");
        Console.WriteLine("precio por llamada "   + llamada.Coste() + " DOP");
    }

    public void GenerarInforme()
    {
        Console.WriteLine("Número total de llamadas: " + numLlamadas);
        Console.WriteLine("Facturación total: " + facturacionTotal.ToString("C") + " DOP ");
    }
}

class Practica2
{
    static void Main(string[] args)
    {
        Centralita centralita = new Centralita();

        LlamadaLocal local1 = new LlamadaLocal("111111111", "222222222", 30);
        centralita.RegistrarLlamada(local1);

        LlamadaProvincial provincial1 = new LlamadaProvincial("333333333", "444444444", 60, 1);
        centralita.RegistrarLlamada(provincial1);

        LlamadaProvincial provincial2 = new LlamadaProvincial("555555555", "666666666", 120, 3);
        centralita.RegistrarLlamada(provincial2);

        centralita.GenerarInforme();


    }
}