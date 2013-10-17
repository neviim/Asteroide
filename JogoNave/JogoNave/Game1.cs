using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JogoNave
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Movimentação nave;
        Movimentação asteroides;

        //Movimentação asteroides1;
        Scrolling scrolling1;
        Scrolling scrolling2;
        SpriteFont pontos;

        //Area do Bot
        Bot asteroideRandom;
        Random rnd = new Random();

        int alturatela, larguraTela;
        int score = 100;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //bonecoRetangulo = new Rectangle(5, posicaoBoneco, bonecoTextura.Width, bonecoTextura.Height);
            // Create a new SpriteBatch, which can be used to draw textures.
            alturatela = GraphicsDevice.Viewport.Height; //altura tela 480
            larguraTela = GraphicsDevice.Viewport.Width; //largura tela 800
          
            spriteBatch = new SpriteBatch(GraphicsDevice);
            nave        = new Movimentação(Content.Load<Texture2D>("nave"), new Rectangle(10, 10, 50, 100), 3);
            asteroides  = new Movimentação(Content.Load<Texture2D>("asteroide"), new Rectangle(850, 300, 180, 180), 3);
            //asteroides1 = new Movimentação(Content.Load<Texture2D>("asteroide"), new Rectangle(850, 100, 180, 180), 3);
            scrolling1  = new Scrolling(Content.Load<Texture2D>("fundo-space"), new Rectangle(0, 0, 1920, 789));
            scrolling2  = new Scrolling(Content.Load<Texture2D>("fundo-space-copia"), new Rectangle(1920, 0, 1920, 789));
            pontos      = Content.Load<SpriteFont>("Vida");

            //Usando a classe Bot
            int diferencaX  = -50; //Acresimo na posição X de nascimento
            int posicaoColX = (larguraTela + diferencaX);   
            int posicaoRndY = rnd.Next(40, (alturatela - 40)); //Posição randomica de Y do nascimento, (Vai variar de 40 ao fim da tela menos 40)

            //Passa para a classe bot em uma variavel publica a altura e a largura da tela
            //asteroideRandom.resolucaoTela(alturatela, larguraTela, diferencaX);

            // Definindo um Asteroide grande em 180
            int asteGrandeLargura    = 180; 
            int asteGrandeAltura     = 180;
            int asteGrandeVelocidade =  3;
 
            //Definindo um Asteroide medio em 100

            //Definindo um Asteroide pequeno em 50

            //Lendo as especificações de um asteroide grande
            //asteroideRandom = new Bot(Content.Load<Texture2D>("asteroide2"), new Rectangle(posicaoColX, posicaoRndY, asteGrandeLargura, asteGrandeAltura), asteGrandeVelocidade);

            asteroideRandom = new Bot(Content.Load<Texture2D>("asteroide2"), new Rectangle(850, 300, 180, 180), 3);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            // apertar ESC para sair do jogo
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            //Tela rolar
            if (scrolling1.retangulo.X + scrolling1.textura.Width <= 0)
            {
                scrolling1.retangulo.X = scrolling2.retangulo.X + scrolling2.textura.Width;
            }
            if (scrolling2.retangulo.X + scrolling2.textura.Width <= 0)
            {
                scrolling2.retangulo.X = scrolling1.retangulo.X + scrolling1.textura.Width;
            }
            
            //executar o metodo update
            scrolling1.Update();
            scrolling2.Update();

            //limita o lado esquerdo da tela
            if (nave.retangulo.X <= 0)
            {
                nave.retangulo.X = 0;
            }

            if (nave.retangulo.X >= 750)
            {
                nave.retangulo.X = (larguraTela - 50);
            }
            
            //limita a parti superior da tela
            if (nave.retangulo.Y <= 0)
            {
                nave.retangulo.Y = 0;
            }
            if (nave.retangulo.Y >= 390)
            {
                nave.retangulo.Y = (alturatela - 90);
            }

            //colissão
            if (nave.retangulo.Intersects(asteroides.retangulo))
            {
                score -= 10;
                nave.retangulo.X = 10;
                asteroides.retangulo.X = 300;
                if (score == 0)
                {
                 this.Exit();
                }
            }
            nave.movimentoPersonagem();
            asteroides.movimentoAsteroide();

            //Area Bot

            //Definindo a colissão
            if (nave.retangulo.Intersects(asteroideRandom.retangulo))
            {
                score -= 10; //Quando colidir perde 10 vida
                nave.retangulo.X = 10; //Quando colidir o personagem vai para a possição 10 do eixo X
                asteroideRandom.retangulo.X = (asteroideRandom.larguraTela + asteroideRandom.Diferenca); //Quando colidir o asteroide vai para posição de nascimento do eixo X

                //Escrever a rotina para fim de jogo na tela e apertando uma tecla para sair
                if (score == 0) 
                {
                    this.Exit();
                }
            }

            //Chama movimentação do asteroide
            asteroideRandom.movimenta();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            scrolling1.Draw(spriteBatch);
            scrolling2.Draw(spriteBatch);
            nave.Draw(spriteBatch);
            asteroides.Draw(spriteBatch);
            spriteBatch.DrawString(pontos, "Vidas: " + score, new Vector2(20, 20), Color.White);

            //Area Bot
            asteroideRandom.Draw(spriteBatch);

            spriteBatch.End();


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
