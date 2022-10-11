using AgendaEF_CF.Context;
using AgendaEF_CF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
                Console.WriteLine("|   opção 5 : ATUALIZAR CONTATO                        |");
                Console.WriteLine("|                                                      |");
                Console.WriteLine("|   opção 0 : SAIR                                     |");
                Console.WriteLine("|______________________________________________________|");
                Console.Write("\nInforme a opção que deseja realizar: ");

                op = Console.ReadLine();
                if (op == "0")
                    return;
                if (op != "1" && op != "2" && op != "3" && op != "4" && op != "5" && op != "0")
                {
                    Console.Clear();
                    Console.WriteLine("Opção inválida!");
                }

            } while (op != "1" && op != "2" && op != "3" && op != "4" && op != "5" && op != "0");

            switch (op)
            {

                case "1":

                    //Insere o contato no banco de dados
                    Console.WriteLine("\nINSERIR CONTATO\n");
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
                    //Lista todos os contatos no banco de dados 
                    Console.WriteLine("\nLISTAR CONTATOS\n");
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
                    //Busca um contato em específico
                    Console.WriteLine("\nBUSCAR CONTATO\n");
                    Console.Write("Digite o Nome: ");
                    string n = Console.ReadLine();

                    Person espFind = new PersonContext().Persons.FirstOrDefault(c => c.Name == n);
                    if (espFind != null) Console.Write(espFind.ToString());
                    else
                    {
                        Console.WriteLine("Contato não encontrado!!!");
                        Thread.Sleep(2000);
                        Console.Clear();
                        Menu();
                        break;
                    }

                    Phone phonesEsp = new PersonContext().Phones.FirstOrDefault(c => c.PersonId == espFind.Id);
                    if (phonesEsp != null) Console.WriteLine(phonesEsp.ToString());

                    Console.WriteLine("Pressione enter para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                    Menu();
                    break;

                case "4":
                    //Deleta um contato
                    Console.WriteLine("\nDELETAR CONTATO\n");
                    using (var context = new PersonContext())
                    {
                        Console.Write("Digite o Nome: ");
                        string del = Console.ReadLine();

                        Person findDel = new PersonContext().Persons.FirstOrDefault(c => c.Name == del);
                        if (findDel != null) Console.WriteLine(findDel.ToString());
                        else
                        {
                            Console.WriteLine("Contato não encontrado!!!");
                            Thread.Sleep(2000);
                            Console.Clear();
                            Menu();
                            break;
                        }

                        Phone phonesDel = new PersonContext().Phones.FirstOrDefault(c => c.PersonId == findDel.Id);
                        if (phonesDel != null) Console.WriteLine(phonesDel.ToString());

                        context.Entry(findDel).State = EntityState.Deleted;
                        context.Persons.Remove(findDel);
                        context.SaveChanges();
                        Console.WriteLine("Contato deletado com sucesso!!!");
                        Thread.Sleep(3000);
                    }
                    Console.Clear();
                    Menu();
                    break;

                case "5":
                    //Atualiza os dados do contato
                    Console.WriteLine("\nATUALIZAR CONTATO\n");
                    using (var context = new PersonContext())
                    {
                        Console.Write("Digite o Nome: ");
                        string atualizar = Console.ReadLine();
                        Person findUp = new PersonContext().Persons.FirstOrDefault(c => c.Name == atualizar);
                        if (findUp != null)
                        {

                            Console.Write("Digite o Email: ");
                            findUp.Email = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Contato não encontrado!!!");
                            Thread.Sleep(2000);
                            Console.Clear();
                            Menu();
                            break;
                        }

                        Phone phonesUp = new PersonContext().Phones.FirstOrDefault(c => c.PersonId == findUp.Id);
                        if (phonesUp != null) 
                        {
                            Console.Write("Digite o Telefone Residêncial: ");
                            phonesUp.Telephone = Console.ReadLine();

                            Console.Write("Digite o Celular: ");
                            phonesUp.Mobile = Console.ReadLine();
                        }

                        context.Entry(findUp).State = EntityState.Modified;
                        context.Persons.AddOrUpdate(findUp);
                        context.Entry(phonesUp).State = EntityState.Modified;
                        context.Phones.AddOrUpdate(phonesUp);
                        context.SaveChanges();

                        Console.WriteLine("Contato atualizado com sucesso!!!");
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
