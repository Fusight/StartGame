﻿using StartGame.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace StartGame
{
    partial class MainGameWindow : Form
    {
        private Random random;

        private Map map;

        private Point selected;

        private Player[] players;
        private Player activePlayer;
        private Player humanPlayer;
        private int activePlayerCounter = 0;

        public MainGameWindow(Map Map, Player Player, int AINumber)
        {
            map = Map;

            selected = new Point(-1, 0);
            random = new Random();

            //Setup players
            players = new Player[AINumber + 1];
            players[0] = Player;
            humanPlayer = Player;
            //Generate AIs
            short botNumber = Convert.ToInt16(Resources.BOTAmount);
            List<string> botNames = new List<string>();
            for (int i = 0; i < botNumber; i++)
            {
                botNames.Add(Resources.ResourceManager.GetString("BOTName" + i));
            }
            for (int i = 0; i < AINumber; i++)
            {
                string name = botNames[random.Next(botNames.Count)];
                players[i + 1] = new Player(PlayerType.computer, name);
                botNames.Remove(name);
            }
            activePlayer = players[0];

            InitializeComponent();

            //Start work to update information in GUI

            //Add players to list
            for (int i = 0; i < players.Length; i++)
            {
                playerList.Items.Add(players[i].Name);
            }

            //Initialise information about player
            playerName.Text = humanPlayer.Name;

            //As it is first turn - set action button to start the game
            nextAction.Text = "Start game!";
        }

        private void MainGameWindow_Load(object sender, EventArgs e)
        {
            gameBoard.Image = map.DrawMapBackground(gameBoard.Width, gameBoard.Height, continentAlpha: 0);
        }

        private void UpdateGameBoard()
        {
            gameBoard.Image = map.DrawMapBackground(gameBoard.Width, gameBoard.Height, continentAlpha: 0);
        }

        public void NextTurn()
        {
            activePlayer = players[activePlayerCounter];
            activePlayer.PlayTurn(nextAction);
            activePlayerCounter = activePlayerCounter == players.Length - 1 ? 0 : activePlayerCounter + 1;
        }

        private void NextAction_Click(object sender, EventArgs e)
        {
            activePlayer.ActionButtonPressed(this);
        }

        private void PlayerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (playerList.SelectedIndex == -1) return;
            Player player = players[playerList.SelectedIndex];
            if (player.Name == activePlayer.Name)
            {
                //Clear all data
                enemyName.Text = "";
            }
            else
            {
                enemyName.Text = player.Name;
            }
        }

        private void MainGameWindow_MouseMove(object sender, MouseEventArgs e)
        {
        }
    }
}