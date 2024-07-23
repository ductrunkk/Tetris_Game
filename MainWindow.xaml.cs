using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tetris1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Mảng các phần tử ảnh có chiều dài và chiều ngang
        // Tạo mảng các ô màu cho block
        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)), 
            new BitmapImage(new Uri("Assets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative)),
            //tạo ảnh hay gọi là tập hợp các pixel mà các pixel này không đổi về kích thước cũng như độ phân giải
            //tạo ảnh dựa trên nguồn Uri
            //đối số thứ nhất là base Uri (đường dẫn gốc)
            //đối số thứ hai là relativeUri để xác định đường dẫn Uri là đường dẫn tuyệt đối hay tương đối
            //trong bài này thì là đường dẫn tương đối
        };

        // Tạo mảng các block
        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-Z.png", UriKind.Relative)),
        };

        // property này dùng để kiểm soát việc vẽ khung trò chơi ra cửa sổ
        private readonly Image[,] imageControls;

        private GameState gameState = new GameState(); //property dùng để kiểm soát trạng thái của trò chơi
        private readonly int maxDelay = 1000; // set maxDelay
        private readonly int minDelay = 200; // set minDelay
        private readonly int delayDecrease = 40; // set thời gian delay giảm sau mỗi lần hoàn thành 1 row
        public MainWindow()
        {
            InitializeComponent(); // Khởi tạo cửa sổ
            imageControls = SetupGameCanvas(gameState.GameGrid); 
            // Vẽ khung trò chơi ra cửa sổ, nhận GameGrid của gameState làm đối số
        }

        private Image[,] SetupGameCanvas(GameGrid grid) // Method dùng để vẽ khung lưới của trò chơi 
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns]; 
            // Mảng 2 chiều có kích thước là hàng và cột của grid trong gameState
            // Khung trò chơi có 22 hàng và 10 cột, mỗi ô dày 25px
            int cellSize = 25; // kích thước của cột

            for(int r = 0; r < grid.Rows; r++) // lặp qua hàng
            {
                for(int c = 0; c < grid.Columns; c++) // lặp qua cột
                {
                    Image imageControl = new Image // Khởi tạo hình ảnh một ô vuông
                    {
                        Width = cellSize,
                        Height = cellSize
                    };

                    Canvas.SetTop(imageControl, (r-2)*cellSize);// set tọa độ trên cho ô
                    Canvas.SetLeft(imageControl, c*cellSize); // set tọa độ trái cho khối ô
                    GameCanvas.Children.Add(imageControl); //thêm child là ô vừa chỉnh tọa độ ,cho khu vực canvas có tên là Gamecanvas trong file MainWindow.xaml
                    imageControls[r, c] = imageControl; // thêm ô đó vào mảng imageControls để có thể sửa đổi hình ảnh tại ô đó ở trong tương lai
                }
            }
            
            return imageControls; // trả về mảng chứa các ô đã khởi tạo
        }

        private void DrawGrid(GameGrid grid) // Phương thức dùng khởi tạo khung lưới của game
        {
            for (int r = 0; r < grid.Rows; r++) 
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r,c]; // gán id = giá trị của ô trong GameGrid 
                    imageControls[r,c].Opacity = 1; // Set độ rõ cho ô là 1
                    imageControls[r,c].Source = tileImages[id]; //gắn biến ImageSource chứa hình ảnh của ô màu vào mảng 2c imageControls để hiển thị trên cửa sổ
                }
            }
        }

        private void DrawBlock(Block block) //Phương thức dùng để vẽ block
        {
            foreach (Position p in block.TilePositions()) // Lặp qua vị trí của từng ô của block
            {
                imageControls[p.Row, p.Column].Opacity = 1; // set cho ô đó có thể nhìn thấy đc
                imageControls[p.Row, p.Column].Source = tileImages[block.Id]; // set cho ô đó màu tương ứng
            }
        }

        private void DrawNextBlock(BlockQueue blockQueue) // Vẽ block tiếp theo
        {
            Block next = blockQueue.NextBlock; // Lấy ra block tiếp theo trong hàng đợi
            NextImage.Source = blockImages[next.Id]; // Gắn Uri tương ứng cho block đó
        }

        private void DrawHeldBlock(Block heldBlock) // Vẽ block đang giữ
        {
            if(heldBlock == null) // Nếu không giữ block nào
            {
                HoldImage.Source = blockImages[0]; // Gắn Uri của empty block 
            }
            else // Còn không
            {
                HoldImage.Source = blockImages[heldBlock.Id]; // Gắn Uri của block tương ứng với id
            }
        }
        private void DrawGhostBlock(Block block) // Vẽ ra block bị hủy do hoàn thành 1 row
        {
            int dropDistance = gameState.BlockDropDistance(); // Khoảng cách
            foreach(Position p in block.TilePositions())
            {
                imageControls[p.Row + dropDistance, p.Column].Opacity = 0.25;
                imageControls[p.Row + dropDistance, p.Column].Source = tileImages[block.Id];
            }
        }
        private void Draw(GameState gameState) // Phương thức dùng để vẽ toàn bộ cửa sổ trò chơi
        {
            DrawGrid(gameState.GameGrid); // vẽ bố cục lưới cho game
            DrawGhostBlock(gameState.CurrentBlock); // vẽ
            DrawBlock(gameState.CurrentBlock); // Vẽ các khối trong grid
            DrawNextBlock(gameState.BlockQueue); // Vẽ khối tiếp theo
            DrawHeldBlock(gameState.HeldBlock); // Vẽ khối đang giữ
            ScoreText.Text = $"Score: {gameState.Score}"; // Hiển thị điểm ra cửa sổ
        }

        private async Task GameLoop() // tạo 1 phương thức bất đồng bộ lặp lại việc vẽ ra cửa sổ khung game
        {
            Draw(gameState); // Khởi tạo khung game

            while(!gameState.GameOver)
            {
                int delay = Math.Max(minDelay, maxDelay - (gameState.Score * delayDecrease)); //điểm càng cao thì delay càng thấp
                await Task.Delay(delay); //cứ mỗi khoảng tgian nhất định sẽ lặp lại việc vẽ và đưa block xuống 1 ô
                gameState.MoveBlockDown();
                Draw(gameState);
            }
            GameOverMenu.Visibility = Visibility.Visible; // khi game over thì hiển thị ra bảng gameovermenu
            FinalScoreText.Text = $"Final Score: {gameState.Score}"; // đồng thời hiển thị ra final score
        }

        private void Window_keydown(object sender, KeyEventArgs e) //tạo event key_down cho class
        {
            if(gameState.GameOver) //game over thì key_down kco tdung
            {
                return;
            }

            switch(e.Key)
            {
                case Key.Left: 
                    gameState.MoveBlockLeft(); //di chuyen block qua trai 1 o
                    break;
                case Key.Right:
                    gameState.MoveBlockRight(); //di chuyen block qua phai 1 o
                    break;
                case Key.Down:
                    gameState.MoveBlockDown(); //di chuyen block xuong 1 o
                    break;
                case Key.Up:
                    gameState.RotateBlockCW(); //xoay block ...
                    break;
                case Key.Z:
                    gameState.RotateBlockCCW(); //xoay block ...
                    break;
                case Key.C:
                    gameState.HoldBlock(); // giữ block lại
                    break;
                case Key.Space:
                    gameState.DropBlock(); // Thả block xuống tức thì
                    break;
                default:
                    return;
            }

            Draw(gameState); // Vẽ lại grid
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e) //event loaded của gamecanvas
        {
            await GameLoop(); // Tức là khi GameCanvas được khởi tạo thì sẽ chạy pthuc GameLoop() để bắt đầu trò chơi
        }
        private async void PlayAgain_Click(object sender, RoutedEventArgs e) //event click để chơi lại
        {
            gameState = new GameState(); //Khởi tạo lại trạng thái game từ đầu
            GameOverMenu.Visibility = Visibility.Hidden; // Ẩn gameovermenu
            await GameLoop(); // Chạy lại pthuc gameloop
        }
    }

}
