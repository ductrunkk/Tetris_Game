namespace Tetris1
{
    public class GameState
    {
        private Block currentBlock;
        // Khi update CurrentBlock thì sẽ gọi tới hàm Reset để set đúng vị trí bắt đầu và độ xoay
        public Block CurrentBlock   
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();

                // Làm cho khi block được spawn ra thì sẽ thấy được hết block chứ không phải 1 nửa block 

                for (int i = 0; i < 2; i++)
                {
                    currentBlock.Move(1, 0);

                    if (!BlockFits())
                    {
                        currentBlock.Move(-1, 0);
                    }

                }
            }
        }
        public GameGrid GameGrid { get; }

        public BlockQueue BlockQueue { get; }

        public bool GameOver { get; private set; }

        public int Score { get; private set;}

        public Block HeldBlock { get; private set; }

        public bool CanHold { get; private set; }

        public GameState()
        {
            // Khởi tạo GameGrid với 22 hàng và 10 cột 
            GameGrid = new GameGrid(22, 10);
            BlockQueue = new BlockQueue();
            CurrentBlock = BlockQueue.GetAndUpdate();
            CanHold = true;
        }
        // Hàm kiểm tra CurrentBlock có ở vị trí hợp lệ hay không 
        private bool BlockFits()
        {
            // Lặp qua từng vị trí của CurrentBlock nếu mà nằm bên ngoài grid thì trả về false ngược lại trả về true 
            foreach (Position p in CurrentBlock.TilePositions())
            {
                if(!GameGrid.IsEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }
            return true;
        }
        // Hàm cho người chơi lựa chọn có muốn lấy block được random ra không
        public void HoldBlock()
        {
            if(!CanHold)
            {
                return;
            }
            // Không có block nào thì gán HeldBlock là CurrentBlock và CurrentBlock là block tiếp theo  
            if(HeldBlock == null)
            {
                HeldBlock = CurrentBlock;
                CurrentBlock = BlockQueue.GetAndUpdate();
            }
            // Nếu block nào được giữ thì đổi CurrentBlock với HeldBlock 
            else
            {
                Block temp = CurrentBlock;
                CurrentBlock = HeldBlock;
                HeldBlock = temp;
            }
            CanHold = false;
        }
        // Hàm xoay CurrentBlock theo chiều kim đồng hồ nếu CurrentBlock ở vị trí không hợp lệ thì xoay nó ngược chiều kim đồng hồ bằng hàm RotateCCW 
        public void RotateBlockCW()
        {
            CurrentBlock.RotateCW();
            if(!BlockFits())
            {
                CurrentBlock.RotateCCW();
            }
        }

        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCCW();

            if(!BlockFits())
            {
                CurrentBlock.RotateCW();
            }
        }
        // Hàm di chuyển các CurrentBlock 
        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);
            // Nếu vị trí di chuyển của CurrentBlock không hợp lệ thì di chuyển ngược trở lại 
            if(!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }

        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);

            if(!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }
        // Hàm kiểm tra game kết thúc chưa
        private bool IsGameOver()
        {
            // Nếu hàng đầu không trống thì game kết thúc thua 
            return !(GameGrid.IsRowEmpty(0) && GameGrid.IsRowEmpty(1));
        }
        // Hàm đặt currrentBlock vào GameGrid và không thể di chuyển block đó xuống 
        private void PlaceBlock()
        {
            // Lặp qua từng TilePositions của CurrentBlock và gán vị trí của GameGrid bằng Id của CurrentBlock  để đánh dấu ô đó đã được sử dụng
            foreach (Position p in CurrentBlock.TilePositions())
            {
                GameGrid[p.Row, p.Column] = CurrentBlock.Id;
            }
            // Tổng số điểm là bằng tổng số hàng được xóa
            Score += GameGrid.ClearFullRows();
            // Kiểm tra game kết thúc thì gán thuộc tính GameOver true
            if (IsGameOver())
            {
                GameOver = true;
            }
            // Game chưa kết thúc thì cho update CurrentBlock là block tiếp theo 
            else
            {
                CurrentBlock = BlockQueue.GetAndUpdate();
                CanHold = true;
            }
        }
        // Hàm di chuyển CurrentBlock xuống dưới
        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);
            // Gọi tới hàm PlaceBlock khi mà vị trí block đi chuyển không hợp lệ để kiểm tra và cập nhật block
            if(!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();

            }
        }
        // Hàm tìm số hàng của block có thể di chuyển xuống dưới 
        private int TileDropDistance(Position p)
        {
            int drop = 0;
            while (GameGrid.IsEmpty(p.Row + drop + 1, p.Column))
            {
                drop++;
            }
        return drop;
        }
        // Hàm lặp qua từng vị trí của CurrentBlock để tìm ra khoảng cách tối thiểu mà block có thể di chuyển xuống dưới 
        public int BlockDropDistance()
        {
            int drop = GameGrid.Rows;
            foreach (Position p in currentBlock.TilePositions())
            {
                drop = System.Math.Min(drop, TileDropDistance(p));
            }
            return drop;
        }
        // Hàm di chuyển block xuống dưới hàng cuối cùng trong grid và kiểm tra trò chơi kết thúc và cập nhật block bằng hàm PlaceBlock
        public void DropBlock()
        {
            CurrentBlock.Move(BlockDropDistance(), 0);
            PlaceBlock();
        }
    }

}
