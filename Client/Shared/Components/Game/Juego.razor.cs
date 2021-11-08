using System.Timers;
using System;
using System.Collections.Generic;
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
        public List<ItemModel> Items { get; set; }
        
        [Parameter]
        public List<PistaModel> Pistas { get; set; }
        
        [Parameter]
        public List<FormaIncorrectaModel> FormasIncorrectas { get; set; }
        
        private Timer gameTimer;
        private int currentFrame { get; set; }

        private string mainCharacterImagePath { get; set; }
        
        protected override void OnInitialized()
        {
            currentFrame = 1;
            mainCharacterImagePath = $"/Images/Game/Main_Character/Character{currentFrame}.png";
            gameTimer = new ();
            gameTimer.Interval = 62;
            gameTimer.Elapsed += TimerOnElapsed;
            gameTimer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            currentFrame = (currentFrame + 1) % 9;
            if (currentFrame == 0)
            {
                currentFrame = 1;
            }
            mainCharacterImagePath = $"/Images/Game/Main_Character/Character{currentFrame}.png";
            StateHasChanged();
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