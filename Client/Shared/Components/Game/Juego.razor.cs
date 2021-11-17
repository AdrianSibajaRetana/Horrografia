using System.Timers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Data.Services.Interfaces;
using Horrografia.Client.Shared.Constants;
using Horrografia.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Horrografia.Client.Shared.Components.Game
{
    public partial class Juego : IDisposable
    {
        [Parameter]
        public NivelModel Nivel { get; set; }
        
        [Parameter]
        public List<ItemModel> Items { get; set; }
        
        [Parameter]
        public List<PistaModel> Pistas { get; set; }
        
        [Parameter]
        public List<FormaIncorrectaModel> FormasIncorrectas { get; set; }
        
        private Timer gameTimer;

        private Queue<ItemModel> ItemQueue { get; set; }

        private ItemModel CurrentItem { get; set; }

        private string CurrentInput { get; set; }

        private int CurrentLife { get; set; }

        private int CurrentMistakes { get; set; }

        private string CurrentLifeString { get; set; }
        private string CurrentLifePercentage { get; set; }
        private string CurrentIncorrectForm { get; set; }
        private int currentFrame { get; set; }
        
        private int CurrentGameScore {get; set;}

        private int currentItem { get; set; }

        private string mainCharacterImagePath { get; set; }
        
        private string currentItemImagePath { get; set; }

        private string currentItemCSS { get; set; }
        
        private bool ShowInstructions {get; set;}
        
        private bool UserWantsToSeeInstructions {get; set;}
        
        private string PistaActual { get; set; }

        private bool ShowClueDialog { get; set; }

        protected override void OnInitialized()
        {
            ItemQueue = new();
            ShowInstructions = false;
            UserWantsToSeeInstructions = false;
            ShowClueDialog = false;
            CurrentIncorrectForm = ""; 
            CurrentGameScore = 0;
            currentFrame = 1;
            currentItem = 1;
            currentItemCSS = "game-item-1";
            CurrentLife = 100;
            CurrentLifeString = CurrentLife.ToString();
            CurrentLifePercentage = CurrentLifeString + '%';
            CurrentMistakes = 0;
            mainCharacterImagePath = $"/Images/Game/Main_Character/Character{currentFrame}.png";
            currentItemImagePath = $"/Images/Game/Items/Item{currentItem}.png";                       
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Task.Delay(100);
                ShowInstructions = true;
                StateHasChanged();
            }
        }

        protected void CloseInstructionDialog()
        {
            ShowInstructions = false;
            UserWantsToSeeInstructions = false;
            StartGame();
        }

        protected void ShowInstructionDialog()
        {
            ShowInstructions = false;
            UserWantsToSeeInstructions = true;
        }
        
        private void StartGame()
        {
            TransformItemListToQueue();
            GetNextItem();
            gameTimer = new ();
            gameTimer.Interval = 62;
            gameTimer.Elapsed += TimerOnElapsed;
            gameTimer.Start();
        }

        private void TransformItemListToQueue()
        {
            var shuffled = Items.OrderBy(x => Guid.NewGuid()).ToList();
            foreach (var item in shuffled)
            {
                ItemQueue.Enqueue(item);
            }
        }

        private void GetNextItem()
        {
            if (ItemQueue.Any())
            {
                var random = new Random();
                CurrentItem = ItemQueue.Dequeue();
                var IncorrectForms = FormasIncorrectas.Where(x => x.Itemid == CurrentItem.Id).ToList();
                int index = random.Next(IncorrectForms.Count());
                CurrentIncorrectForm = IncorrectForms[index].Forma;
            }
            else
            {
                CurrentIncorrectForm = "";
                Console.Write("Ganó la partida");
            }
        }

        private void CheckAnswer()
        {
            if (CurrentItem.FormaCorrecta == CurrentInput)
            {
                Console.Write("Intentó bien");
            }
            else
            {
                CurrentLife -= 100 / Nivel.ErroresPermitidos;
                CurrentLifeString = CurrentLife.ToString();
                CurrentLifePercentage = CurrentLifeString + '%';
                CurrentMistakes++;
                if (CurrentLife <= 0 || CurrentMistakes == Nivel.ErroresPermitidos)
                {
                    CurrentLife = 0;
                    CurrentLifeString = CurrentLife.ToString();
                    CurrentLifePercentage = CurrentLifeString + '%';
                    Console.Write("Perdió la partida");
                }
            }
            CurrentInput = "";
            GetNextItem();
        }

        private void ShowClue()
        {
            var pistaObject = Pistas.FirstOrDefault(p => p.Id == CurrentItem.PistaId);
            PistaActual = pistaObject == null ? string.Empty : pistaObject.Pista;
            ShowClueDialog = true;
        }
        
        protected void CloseClueDialog()
        {
            ShowClueDialog = false;
        }
        

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            currentFrame = (currentFrame + 1) % 9;
            if (currentFrame == 0)
            {
                currentFrame = 1;
            }

            if (currentFrame == 1 || currentFrame == 4)
            {
                ChangeItemDisplayed();
            }

            mainCharacterImagePath = $"/Images/Game/Main_Character/Character{currentFrame}.png";
            StateHasChanged();
        }

        private void ChangeItemDisplayed()
        {
            if (currentItem == 1)
            {
                currentItem = 2;
                currentItemCSS = "game-item-2";
            }
            else
            {
                currentItem = 1;
                currentItemCSS = "game-item-1";
            }
            currentItemImagePath = $"/Images/Game/Items/Item{currentItem}.png";
        }

        public void Dispose()
        {
            if (gameTimer != null)
            {
                gameTimer.Dispose();
            }
        }
    }
}