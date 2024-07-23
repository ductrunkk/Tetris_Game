namespace Tetris1
{
    public class GameGrid
    {
        // Khai báo một mảng hai chiều kiểu int để lưu trữ trạng thái của lưới game
        private readonly int[,] grid; 

        // Số hàng của lưới game.
        public int Rows { get; } 

        // Số cột của lưới game.
        public int Columns { get; } 

        // Định nghĩa một chỉ mục để truy cập trực tiếp vào các ô của lưới.
        public int this[int r, int c] 
        {
            // Trả về giá trị tại ô (r, c).
            get => grid[r, c]; 
            // Đặt giá trị tại ô (r, c).
            set => grid[r, c] = value; 
        }

        // Phương thức khởi tạo lưới game với số hàng và số cột cho trước.
        public GameGrid(int rows, int columns) 
        {
            Rows = rows;
            Columns = columns;
            
            // Khởi tạo mảng hai chiều với số hàng và số cột đã cho.
            grid = new int[Rows, Columns]; 
        }

        // Kiểm tra xem một ô có nằm trong lưới game hay không.
        public bool IsInside(int r, int c) 
        {
            return r >= 0 && r < Rows && c >= 0 && c < Columns;
        }

       // Kiểm tra xem một ô có trống hay không.
        public bool IsEmpty(int r, int c) 
        {
            return IsInside(r, c) && grid[r, c] == 0;
        }
        
        // Kiểm tra xem một hàng có đầy hay không.
        public bool IsRowFull(int r) 
        {
            for(int c = 0; c < Columns; c++)
            {
                if (grid[r, c] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        // Kiểm tra xem một hàng có trống hay không.
        public bool IsRowEmpty(int r) 
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        // Xóa hàng
        // Xóa một hàng bằng cách đặt tất cả các ô trong hàng đó thành 0.
        private void ClearRow(int r) 
        {
            for(int c = 0; c < Columns; c++)
            {
                grid[r, c] = 0;
            }
        }

        // Di chuyển một hàng xuống một số hàng cho trước.
        private void MoveRowDown(int r, int numRows) 
        {
            for (int c = 0;c < Columns; c++)
            {
                grid[r + numRows, c] = grid[r, c];
                grid[r, c] = 0;
            }
        }

       // Xóa tất cả các hàng đầy trong lưới và trả về số hàng đã xóa.
        public int ClearFullRows() 
        {
            int cleared = 0;

            for(int r = Rows-1; r >= 0; r--)
            {
                if (IsRowFull(r))
                {
                    ClearRow(r);
                    cleared++;
                }
                else if(cleared > 0)
                {
                    MoveRowDown(r, cleared);
                }
            }
            return cleared;
        }
    }
}

