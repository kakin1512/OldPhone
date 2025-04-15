using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace OldPhoneApp
{
    public partial class Form1 : Form
    {
        //Old key-letter mapping
        private readonly Dictionary<char, string> KeyMapping = new()
        {
            { '1', "&'(" },
            { '2', "ABC" },
            { '3', "DEF" },
            { '4', "GHI" },
            { '5', "JKL" },
            { '6', "MNO" },
            { '7', "PQRS" },
            { '8', "TUV" },
            { '9', "WXYZ" },
            { '0', " " }
        };
        //user text at the moment
        private string currentText = "";
        //current sequence of keys pressed
        private string sequence = "";
        //last key pressed to compare if the user keeps pressing the same key
        private char lastKey = '\0';
        //timer to detecte pauses between letters
        private readonly System.Windows.Forms.Timer pressTimer = new();
        private TextBox? txtDisplay;



        public Form1()
        {
            InitializeComponent(); //star the form
            InitializeKeypad(); //create the buttons
            InitializeTimer(); //set the timer
        }

        private void InitializeKeypad()
        {
            string[] keys =
            [
                "1 \n&'(",
                "2 \nabc",
                "3 \ndef",
                "4 \nghi",
                "5 \njkl",
                "6 \nmno",
                "7 \npqrs",
                "8 \ntuv",
                "9 \nwxyz",
                "* \n<",
                "0 \n  ",
                "# \n>"
            ];

            //Creates the keyboard buttons with their properties and adds them to the form

            int index = 0;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 3; col++)  // 4x2 keypad layoud
                {
                    //create individual button 
                    Button btn = new()
                    {
                        Text = keys[index],
                        Tag = keys[index],
                        Width = 60,
                        Height = 60,
                        BackColor = Color.AntiqueWhite,
                        Left = 40 + col * 80,
                        Top = 80 + row * 70,
                        Font = new Font("Times New Roman", 12)
                        
                    };
                    btn.Click += Btn_Click;
                    this.Controls.Add(btn);
                    index++;
                }
            }
            // create the display box where the user text appears
            txtDisplay = new TextBox
            {
                Name = "txtDisplay",
                Top = 20,
                Left = 20,
                Width = 270,
                Font = new Font("Segoe UI", 16),
                ReadOnly = true
            };
            this.Controls.Add(txtDisplay);
        }
        private void InitializeTimer()
        {
            pressTimer.Interval = 1000;  // instantiation avoids NullReferenceException
            pressTimer.Tick += (s, e) => //wait 1 second
            {
                FinishSequence();
                pressTimer.Stop();
            };
        }
        // Separate method for readability and reusability
        private void OnPressTimerTick(object sender, EventArgs e)
        {
            FinishSequence();
            pressTimer.Stop(); // Stop the timer until the next input
        }

        //click handler for buttons
        private void Btn_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn || btn.Tag is null)
                return; //ensure sender is a button and tag is not null

            string? tagValue = btn.Tag.ToString();
            if (string.IsNullOrEmpty(tagValue)) 
                return; //Ensure tagValue is noy null or empty

            char key = tagValue[0]; //get the first character of the key text

            if (key == '*')
            {
                if (currentText.Length > 0) //deletes the last character of the text
                    currentText = currentText[..^1];
                UpdateDisplay();
                return;
            }
            // '#' acts as a "send"/"confirm"
            if (key == '#')
            {
                FinishSequence(); 
                MessageBox.Show("Final text: " + currentText, "Result"); //ends the sequence and displays the final message
                return;
            }
            // if same key is pressed again, build up th sequence
            if (key == lastKey)
            {
                sequence += key;
            }
            else
            {
                FinishSequence(); //previous key sequence ends
                sequence = key.ToString(); //start new sequence
                lastKey = key;
            }

            pressTimer.Stop(); //restart timer
            pressTimer.Start();
        }
        // when a pause is detected, select the appropriate letter
        private void FinishSequence()
        {
            if (sequence.Length == 0)
                return;

            char digit = sequence[0];
            if (KeyMapping.TryGetValue(digit, out string? letters))
            {
                int index = (sequence.Length - 1) % letters.Length; //cycle through letters
                currentText += letters[index]; //add selected letter
                UpdateDisplay(); //update the screen
            }

            sequence = ""; //reset for next key
            lastKey = '\0';
        }

        private void UpdateDisplay()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox txt && txt.Name == "txtDisplay")
                {
                    txt.Text = currentText;
                    break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
