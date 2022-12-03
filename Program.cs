using System;
using System.Runtime.CompilerServices;
using System.Threading;
using SFML.Learning;
using SFML.Graphics;
using SFML.Window;

namespace Cards
{
    internal class Program : Game
    {
        static string[] iconsName;

        static int[,] cards;
        static int cardCount = 20;
        static int cardWidth = 100;
        static int cardHeight = 100;

        static int countPerLune = 5;
        static int space = 40;
        static int leftOffset = 70;
        static int topOffset = 20;

        static string clikCard = LoadSound("knopka-schelchok-dalekii-nejnyii-myagkii1.wav");
        static string closeCards = LoadSound("kartyi-v-rukah-umelogo-kartejnika.wav");
        static string closeBedCards = LoadSound("iz-kolodyi-vzyali-odnu-kartu.wav");
        static string winSaund = LoadSound("zvuk-pobedyi-vyiigryisha.wav");
        static string fallSaund = LoadSound("us_wawa.wav");

        static void LoadIcons()
        {
            iconsName = new string[7];

            iconsName[0] = LoadTexture("images.png");

            for (int i = 1; i < iconsName.Length ; i++)
            {

                iconsName[i] = LoadTexture("Icon_" + (i).ToString() + ".png");
            }
        }


        static void Shuffle(int[] arr)
        {
            Random  random = new Random();

            for (int i = arr.Length -1; i>=1 ; i--)
            {
                int j = random.Next(1, i + 1);

                int temp = arr[j];
                arr[j] = arr[i];
                arr[i] = temp;
            }
        }

        static void InitCard()
        {
            Random random = new Random();
            cards = new int[cardCount, 6];

            int[] iconid = new int[cards.GetLength(0)];

            int id = 0;

            for (int i = 0; i < iconid.Length; i++)
            {
                if (i % 2 == 0)
                {
                    id = random.Next(1, 7);
                }
                iconid[i] = id;
            }

            Shuffle(iconid);
            Shuffle(iconid);
            Shuffle(iconid);

            for (int i = 0; i < cards.GetLength(0); i++)
            {
                cards[i, 0] = 0; //state
                cards[i, 1] = (i % countPerLune) * (cardWidth + space) + leftOffset; // posX
                cards[i, 2] = (i / countPerLune) * (cardHeight + space) + topOffset; // posY
                cards[i, 3] = cardWidth; // Width
                cards[i, 4] = cardHeight; // Height

                
                cards[i, 5] = iconid[i]; // id
            }
        }

        static void SetStateToAllCards(int state)
        {
            for (int i = 0; i < cards.GetLength(0); i++)
            {
                cards[i, 0] = state; 
               
            }
        }

        

        static void DrowCards()
        {
            for (int i = 0; i < cards.GetLength(0); i++)
            {
                if (cards[i, 0] == 1) // open
                {
                    DrawSprite(iconsName[cards[i, 5]], cards[i, 1], cards[i, 2]);

                    //if (cards[i, 5] == 1)
                    //{SetFillColor(0, 100, 0);FillRectangle(cards[i, 1], cards[i, 2], cards[i, 3], cards[i, 4]);}
                    //if (cards[i, 5] == 2)
                    //{ SetFillColor(0, 100, 100); FillRectangle(cards[i, 1], cards[i, 2], cards[i, 3], cards[i, 4]); }
                    //if (cards[i, 5] == 3)
                    //{ SetFillColor(0, 0, 100); FillRectangle(cards[i, 1], cards[i, 2], cards[i, 3], cards[i, 4]); }
                    //if (cards[i, 5] == 4)
                    //{ SetFillColor(100, 100, 0); FillRectangle(cards[i, 1], cards[i, 2], cards[i, 3], cards[i, 4]); }
                    //if (cards[i, 5] == 5)
                    //{ SetFillColor(100, 100, 100); FillRectangle(cards[i, 1], cards[i, 2], cards[i, 3], cards[i, 4]); }
                    //if (cards[i, 5] == 6)
                    //{ SetFillColor(100, 0, 100); FillRectangle(cards[i, 1], cards[i, 2], cards[i, 3], cards[i, 4]); }
                }

                    if (cards[i, 0] == 0) // close
                    {
                    DrawSprite(iconsName[0], cards[i, 1], cards[i, 2]);
                    //SetFillColor(30, 30, 30); FillRectangle(cards[i, 1], cards[i, 2], cards[i, 3], cards[i, 4]);
                    }


               
            }
        }

        static int GetIndexCardByMausePosition()
        {
            for (int i = 0; i < cards.GetLength(0); i++)
            {
                if (MouseX >= cards[i,1] && MouseX <= cards[i,1] + cards[i,3] && MouseY >= cards[i,2] && MouseY <= cards[i,2] + cards[i,4])
                {
                    return i;
                }

                
            }
            return -1;
        }

       

        static int countDown ;
       static bool isChoose = true;

        static bool gameOver = false;
        static bool isWin = false;
            

       static void PrintTime(object state)
        {
            
            countDown--;
            

        }
        static void Main(string[] args)
        {
            TimerCallback timeCB = new TimerCallback(PrintTime);

            Timer time = new Timer(timeCB, null, 0, 1000);
            while (gameOver == false)
            {



                int openCardAmount = 0;
                int firstOpenCardIndex = -1;
                int secondOpenCardIndex = -1;
                int remainingCard = cardCount;

                LoadIcons();

                SetFont("comic.ttf");

                InitWindow(800, 600, "Card");

                ClearWindow(26, 46, 92);
                SetFillColor(250, 250, 250);
                DrawText(200, 100, "Выбери уровень сложности!", 24);
                SetFillColor(45, 250, 30);
                DrawText(200, 180, "Нажми букву \"H\" - Hard level", 18);
                DrawText(200, 220, "Нажми букву \"M\" - Middle level", 18);
                DrawText(200, 260, "Нажми букву \"E\" - Easy level", 18);
                DisplayWindow();
                Delay(1);

                

                while (true)
                {
                    if (GetKey(Keyboard.Key.H))
                    {
                        countDown = 35;
                        
                        isChoose = false;
                        break;
                    }
                    if (GetKey(Keyboard.Key.M))
                    {
                        countDown = 50;
                        
                        isChoose = false;
                        break;
                    }
                    if (GetKey(Keyboard.Key.E))
                    {
                        countDown = 65;
                        
                        isChoose = false;
                        break;
                    }

                }

                while (isChoose == false)
                {
                    InitCard();
                    SetStateToAllCards(1);

                    ClearWindow(26, 46, 92);

                    DrowCards();
                    DisplayWindow();
                    Delay(5000);
                    PlaySound(closeCards);
                    SetStateToAllCards(0);

                    




                    while (true)
                    {

                        DispatchEvents();
                        if (gameOver == false)
                        {





                            if (countDown == 0)
                            {
                                gameOver = true;
                                isWin = false;
                                isChoose = true;
                                break;
                            }

                            if (remainingCard == 0)
                            {
                                gameOver = true;
                                isWin = true;
                                isChoose = true;
                                break;
                            }


                            if (openCardAmount == 2)
                            {
                                if (cards[firstOpenCardIndex, 5] == cards[secondOpenCardIndex, 5])
                                {
                                    cards[firstOpenCardIndex, 0] = -1;
                                    cards[secondOpenCardIndex, 0] = -1;

                                    remainingCard -= 2;
                                }
                                else
                                {
                                    cards[firstOpenCardIndex, 0] = 0;
                                    cards[secondOpenCardIndex, 0] = 0;
                                }

                                firstOpenCardIndex = -1;
                                secondOpenCardIndex = -1;
                                openCardAmount = 0;

                                Delay(1000);
                                PlaySound(closeBedCards);
                            }

                            if (GetMouseButtonDown(0) == true)
                            {

                                int index = GetIndexCardByMausePosition();

                                if (index != -1 && index != firstOpenCardIndex)
                                {
                                    PlaySound(clikCard);
                                    cards[index, 0] = 1;

                                    openCardAmount++;

                                    if (openCardAmount == 1) firstOpenCardIndex = index;
                                    if (openCardAmount == 2) secondOpenCardIndex = index;
                                }
                            }
                        }


                        ClearWindow(26, 46, 92);


                        DrowCards();

                        SetFillColor(250, 250, 250);
                        DrawText(500, 560, "Время осталось: " + countDown, 24);



                        DisplayWindow();
                        Delay(1);

                    }
                    

                    if (isWin == true)
                    {
                        ClearWindow();
                        PlaySound(winSaund);
                        SetFillColor(250, 250, 250);
                        DrawText(200, 300, "Поздравляю! Ты открыл все карты!", 24);

                        SetFillColor(45, 250, 30);
                        DrawText(200, 350, "Для перезапуска игры нажми букву \"R\" !", 18);



                        DisplayWindow();
                        Delay(5000);

                    }
                    else
                    {
                        ClearWindow();
                        PlaySound(fallSaund);
                        SetFillColor(250, 250, 250);
                        DrawText(300, 300, "Ты проиграл!!!", 24);

                        SetFillColor(45, 250, 30);
                        DrawText(200, 350, "Для перезапуска игры нажми букву \"R\" !", 18);



                        DisplayWindow();
                        Delay(1000);

                    }
                    if (gameOver == true)
                    {
                        while (true)
                        {


                            if (GetKey(Keyboard.Key.R))
                            {

                                countDown = 0;
                                isChoose = true;
                                isWin = false;

                                gameOver = false;
                                break;
                            }
                        }
                    }

                }


            }   
            
            Console.ReadLine();
        }

        
    }
}
