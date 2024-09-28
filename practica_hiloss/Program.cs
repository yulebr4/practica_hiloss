using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace practica_hiloss
{
    internal class Program
    {
        static int balance = 1000;
        static object lockbalance = new object();
        static Random random = new Random();  

        static void Main(string[] args)
        {
            // Esto nos ayudara para mostrar el saldo inicial
            Console.WriteLine($"El saldo inicial de la cuenta es: {balance}");


            // Esta clase nos ayudara a iniciar los hilos
            Thread t1 = new Thread(Withdraw);
            Thread t2 = new Thread(Withdraw);

            t1.Start();
            t2.Start();


            Console.WriteLine("Todas las transacciones han sido procesadas.");
            Console.ReadLine();
        }

        static void Withdraw()
        {

            // El for nos ayudara a que cada hilo intentara realizar 20 retiros
            for (int i = 0; i < 20; i++)  
            {

                // Esta variable nos ayudara a generar una cantidad aleatoria para retirar entre $10 y $100
                int cantidadRetiro;


                lock (random)  
                {

                    // Aqui generara un numero aleatorio entre 10 y 100 ()
                    cantidadRetiro = random.Next(10, 101);  
                }

               
                lock (lockbalance)
                {
                    if (balance >= cantidadRetiro)  
                    {
                        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} va a retirar {cantidadRetiro}.");
                        balance -= cantidadRetiro;
                        Console.WriteLine($"Nuevo balance después del retiro: {balance}");

                        
                        Console.WriteLine("La transacción fue exitosa.");
                    }
                    else
                    {
                        // Con la ayuda de este writeline nos mandara un mensaje por pantalla cuando no haya fondo suficiente 
                        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} No puede retirar. Fondos insuficientes.");
                        break;
                    }
                }

                // El metodo thread.sleep nos asegurara de que haya un periodo de tiempo en cada transaccion
                Thread.Sleep(2000);
            }
        }
    }
}