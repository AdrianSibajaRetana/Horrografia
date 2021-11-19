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
using System.Web;
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

        [Parameter] 
        public string PlayerID { get; set; }

        private Timer gameTimer;

        private Queue<ItemModel> ItemQueue { get; set; }

        private ItemModel CurrentItemModel { get; set; }

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

        private bool ShowItemGuessedDialog { get; set; }

        private bool IsCurrentGuessGood { get; set; }
        
        private string CurrentFormaCorrecta { get; set; }

        private bool ShowGameoverDialog { get; set; }
        
        private bool DidUserWinGame { get; set; }
        
        private Stopwatch GameStopWatch { get; set; }
        
        private ReporteModel GameReport { get; set; }

        private List<ContieneErrorModel> GameMistakes;

        protected override void OnInitialized()
        {
            ItemQueue = new();
            GameMistakes = new();
            ShowInstructions = false;
            UserWantsToSeeInstructions = false;
            ShowClueDialog = false;
            ShowItemGuessedDialog = false;
            IsCurrentGuessGood = false;
            ShowGameoverDialog = false;
            DidUserWinGame = false;
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
                CurrentItemModel = ItemQueue.Dequeue();
                CurrentFormaCorrecta = CurrentItemModel.FormaCorrecta;
                var IncorrectForms = FormasIncorrectas.Where(x => x.Itemid == CurrentItemModel.Id).ToList();
                int index = random.Next(IncorrectForms.Count());
                CurrentIncorrectForm = IncorrectForms[index].Forma;
                GameStopWatch = Stopwatch.StartNew();
            }
            else
            {
                CurrentIncorrectForm = "";
                ShowGameOverScreen(true);
            }
        }

        private void CheckAnswer()
        {
            GameStopWatch.Stop();
            if (CurrentFormaCorrecta == CurrentInput)
            {
                ShowGuessDialog(true);
                //Give user score here.
                var timeElapsedSeconds = GameStopWatch.Elapsed.Seconds;
                if (timeElapsedSeconds >= 60)
                {
                    timeElapsedSeconds = 1;
                }
                else
                {
                    timeElapsedSeconds = 60 - timeElapsedSeconds;
                }

                CurrentGameScore += (timeElapsedSeconds * 500);
            }
            else
            {
                ContieneErrorModel Mistake = new();
                Mistake.IdItem = CurrentItemModel.Id;
                Mistake.Respuesta = CurrentInput;
                GameMistakes.Add(Mistake);
                CurrentLife -= 100 / Nivel.ErroresPermitidos;
                CurrentLifeString = CurrentLife.ToString();
                CurrentLifePercentage = CurrentLifeString + '%';
                CurrentMistakes++;
                if (CurrentLife <= 0 || CurrentMistakes == Nivel.ErroresPermitidos)
                {
                    CurrentLife = 0;
                    CurrentLifeString = CurrentLife.ToString();
                    CurrentLifePercentage = CurrentLifeString + '%';
                    ShowGameOverScreen(false);
                }
                else
                {
                    ShowGuessDialog(false);
                }
            }
        }

        private void ShowGameOverScreen(bool didUserWin)
        {
            DidUserWinGame = didUserWin;
            ShowGameoverDialog = true;
        }

        protected async Task SubmitGame()
        {
            //Do this method.
            GameReport.IdUsuario = PlayerID;
            GameReport.Puntuacion = CurrentGameScore;
            GameReport.IdNivel = Nivel.Id;
            GameReport.CantidadErrores = CurrentMistakes;
            // Push Game report
            // Get game report ID
            
        }
        
        

        private void ShowGuessDialog(bool isCorrect)
        {
            IsCurrentGuessGood = isCorrect;
            ShowItemGuessedDialog = true;
        }
        
        protected void CloseGuessDialog()
        {
            CurrentInput = "";
            GetNextItem();
            ShowItemGuessedDialog = false;
        }

        private void ShowClue()
        {
            var pistaObject = Pistas.FirstOrDefault(p => p.Id == CurrentItemModel.PistaId);
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