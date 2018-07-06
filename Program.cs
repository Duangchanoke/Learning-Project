using System;
using System.Collections.Generic;
using ServiceStack.Redis;
using ServiceStack.Text;

namespace TaskHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isStay = true; int count = 0;

            Console.WriteLine("-- Welcome to Your To-Do List --");
            TaskList toDoList = new TaskList();
            while (isStay)
            {
                Console.WriteLine("\nSelect the menus : ");
                Console.WriteLine("Press spacebar to see list and exit");
                Console.WriteLine("Press '1' to add your new task");
                Console.WriteLine("Press '2' to drop your completed task");
                ConsoleKeyInfo menu = Console.ReadKey();

                if (char.IsDigit(menu.KeyChar))
                {
                    int menuNumber = int.Parse(menu.KeyChar.ToString());

                    if (menuNumber.Equals(1))
                    {
                        Console.WriteLine("\nPlease enter your task : ");
                        string taskName = Console.ReadLine();
                        toDoList.AddTaskList(id: ++count, name: taskName);
                    }
                    else if (menuNumber.Equals(2))
                    {
                        toDoList.PrintTaskList();
                        Console.WriteLine("\nPlease enter a completed task : ");
                        ConsoleKeyInfo taskId = Console.ReadKey();

                        if (char.IsDigit(taskId.KeyChar))
                        {
                            int taskNumber = int.Parse(taskId.KeyChar.ToString());
                            if (taskNumber > 0 && taskNumber <= toDoList.CountTaskList())
                            {
                                toDoList.CompleteTaskList(taskNumber);
                            }
                            else
                            {

                                Console.WriteLine("\nNo task id defined. Please select a number in the list");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid task id. Please select a number in the list");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid menu. Please press only these numbers '1', '2'");
                    }
                }
                else if (menu.Key.Equals(ConsoleKey.Spacebar))
                {
                    isStay = false;
                    toDoList.PrintTaskList();
                }
                else
                {
                    Console.WriteLine("\nInvalid menu. Please press only '1', '2', spacebar");
                }
            }
        }
    }

    class TaskList
    {
        List<Task> TaskRecord = new List<Task>();

        public void AddTaskList(int id, string name)
        {
            TaskRecord.Add(new Task(id, name));
            Console.WriteLine("Your new task has been added !");
        }

        public void PrintTaskList()
        {
            if (TaskRecord.Count > 0)
            {
                Console.WriteLine("- Your All To-Do List -");
                for (int i = 0; i < TaskRecord.Count; i++)
                {
                    Console.WriteLine("\n" + TaskRecord[i].GetId() + " - " + TaskRecord[i].GetName() + " | IsCompleted : " + TaskRecord[i].GetIsCompleted());
                }
            }
            else
            {
                Console.WriteLine("\nYou have not added any tasks yet !");
            }
        }

        public void CompleteTaskList(int id)
        {
            if (TaskRecord.Count > 0)
            {
                id -= 1;
                TaskRecord[id].CloseTask();
                Console.WriteLine("\nYour task has been completed !");
            }
            else
            {
                Console.WriteLine("\nYou have no task to delete !");
            }
        }

        public int CountTaskList()
        {
            return TaskRecord.Count;
        }
    }

    class Task
    {
        private int Id { get; set; }
        private string Name { get; set; }
        private bool IsCompleted { get; set; }

        public Task(int id, string name)
        {
            Id = id;
            Name = name;
            IsCompleted = false;

        }
        public int GetId()
        {
            return Id;
        }

        public string GetName()
        {
            return Name;
        }

        public bool GetIsCompleted()
        {
            return IsCompleted;
        }

        public void CloseTask()
        {
            IsCompleted = true;
        }
    }
}
 