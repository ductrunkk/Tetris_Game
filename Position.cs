namespace Tetris1
{
     //Khai báo lớp Position
    public class Position
    {
        //Khai báo thuộc tính Row để lưu trữ hàng của 1 ô trong lưới game
        public int Row {  get; set; }

        //Khai báo thuộc tính Column để lưu trữ cột của 1 ô trong lưới game
        public int Column { get; set; }

        //Phương thức khởi tạo cho lớp Position với hai tham số là hàng và cột
        public Position(int row, int column)
        {
            //Gán giá trị cho thuộc tính Row
            Row = row;
            //Gán giá trị cho thuộc tính Column
            Column = column;
        }
    }
    
}

