using AgendaEF_CF.Context;
using AgendaEF_CF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgendaEF_CF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            string op;

            do
            {

                Console.WriteLine("\n|°°°°°°°°°°°°°°°°°°°°°°° AGENDA °°°°°°°°°°°°°°°°°°°°°°°|");
                Console.WriteLine("|                                                      |");
                Console.WriteLine("|°°°°°°°°°°°°°°°°°°  MENU DE OPÇÕES  °°°°°°°°°°°°°°°°°°|");
                Console.WriteLine("|                                                      |");
                Console.WriteLine("|   opção 1 : ADICIONAR CONTATO                        |");
                Console.WriteLine("|   opção 2 : CONSULTAR TODOS OS CONTATOS              |");
                Console.WriteLine("|   opção 3 : CONSULTAR CONTATO ESPECÍFICO             |");
                Console.WriteLine("|   opção 4 : DELETAR CONTATO                          |");
                Console.WriteLine("|   opção 0 : SAIR                                     |");
                Console.WriteLine("|______________________________________________________|");
                Console.Write("\nInforme a opção que deseja realizar: ");

                op = Console.ReadLine();
                if (op == "0")
                    return;
                if (op != "1" && op != "2" && op != "3" && op != "4" && op != "0")
                {
                    Console.Clear();
                    Console.WriteLine("Opção inválida!");
                }

            } while (op != "1" && op != "2" && op != "3" && op != "4" && op != "0");

            switch (op)
            {

                case "1":
                    Console.WriteLine("\nINSERIR USUÁRIO\n");
                    Console.Write("Digite o Nome: ");
                    string nome = Console.ReadLine();

                    Person find = new PersonContext().Persons.FirstOrDefault(c => c.Name == nome);
                    if (find != null)
                    {
                       
                        Console.Clear();
                        Console.WriteLine("Este nome já esta cadastrado!!!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        Menu();
                        break;
                    }

                    Console.Write("Digite o Telefone Residêncial: ");
                    string telRes = Console.ReadLine();

                    Console.Write("Digite o Celular: ");
                    string celular = Console.ReadLine();

                    Console.Write("Digite o Email: ");
                    string email = Console.ReadLine();

                    using (var context = new PersonContext())
                    {

                        context.Persons.Add(new Person()
                        {
                            Name = nome,
                            Email = email,
                            Phones = new List<Phone>()
                            {
                                 new Phone()
                                 {
                                    Telephone = telRes,
                                    Mobile = celular,
                                 }
                            }
                        });
                        context.SaveChanges();
                    }
                    Console.WriteLine("Contato salvo com sucesso!!!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    Menu();
                    break;

                case "2":
                    Console.WriteLine("\nLISTAR USUÁRIOS\n");
                    using (var context = new PersonContext())
                    {
                        var persons = new PersonContext().Persons.ToList();
                        foreach (var person in persons)
                        {
                            Console.Write(person.ToString());
                            Phone phones = new PersonContext().Phones.FirstOrDefault(c => c.PersonId == person.Id);
                            if (phones != null) Console.WriteLine(phones.ToString());
                            
                        }
                       
                        Console.WriteLine("Pressione enter para continuar...");
                        Console.ReadKey();
                    }
                    Console.Clear();
                    Menu();
                    break;

                case "3":
                    Console.WriteLine("\nBUSCAR USUÁRIO\n");
                    Console.Write("Digite o Nome: ");
                    string n = Console.ReadLine();
                    Person espFind = new PersonContext().Persons.FirstOrDefault(c => c.Name == n);
                    if (espFind != null) Console.WriteLine(espFind.ToString());
                    Console.Clear();
                    Menu();
                    break;

                case "4":
                    Console.WriteLine("\nDELETAR USUÁRIO\n");
                    using (var context = new PersonContext())
                    {
                        Console.Write("Digite o Nome: ");
                        string del = Console.ReadLine();
                        Person findDel = new PersonContext().Persons.FirstOrDefault(c => c.Name == del);
                        if (findDel != null) Console.WriteLine(findDel.ToString());

                        context.Entry(findDel).State = EntityState.Deleted;
                        context.Persons.Remove(findDel);
                        context.SaveChanges();
                        Console.WriteLine("Contato deletado com sucesso!!!");
                        Thread.Sleep(3000);
                    }
                    Console.Clear();
                    Menu();
                    break;


                case "0":
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
