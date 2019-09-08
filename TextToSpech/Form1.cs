using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextToSpech
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SpeechRecognitionEngine _recognizer;

        private void button2_Click(object sender, EventArgs e)
        {
            PromptBuilder builder = new PromptBuilder();

            builder.StartStyle(new PromptStyle(PromptVolume.ExtraLoud));
            builder.StartStyle(new PromptStyle(PromptRate.Slow));

            builder.StartVoice(VoiceGender.Male, VoiceAge.Child);
            builder.AppendText(richTextBox1.Text);
           
            builder.EndVoice();
            builder.EndStyle();
            builder.EndStyle();

            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Speak(builder);
            synthesizer.Dispose();
        }


        void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "exit")
            {
                Application.Exit();
                return;
            }
         setText(e.Result.Text);
        }

        public void setText(string Text)
        {
            richTextBox1.Text = Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
          _recognizer.RecognizeAsync(RecognizeMode.Multiple);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _recognizer = new SpeechRecognitionEngine();
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append("I"); // add "I"
            grammarBuilder.Append(new Choices("like", "dislike")); // load "like" & "dislike"
            grammarBuilder.Append(new Choices("dogs", "cats", "birds", "snakes",
               "fishes", "tigers", "lions", "snails", "elephants")); // add animals
            _recognizer.LoadGrammar(new Grammar(grammarBuilder));

            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.SpeechRecognized += _recognizer_SpeechRecognized;
           
        }
    }
}
