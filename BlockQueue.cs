using System;
namespace Tetris1
{
    //Khai báo lớp BlockQueue
    public class BlockQueue
    {
        //Khai báo các khối trong game
        private readonly Block[] blocks = new Block[]
        {
            //Tạo một khối I mới
            new IBlock(),
            //Tạo một khối J mới
            new JBlock(),
            //Tạo một khối L mới
            new LBlock(),
            //Tạo một khối S mới
            new SBlock(),
            //Tạo một khối T mới
            new TBlock(),
            //Tạo một khối Z mới
            new ZBlock(),
            //Tạo một khối O mới
            new OBlock()
        };
        //Khởi tạo đối tượng Random để chọn khối ngẫu nhiên
        private readonly Random random = new Random();

        //Khai báo thuộc tính NextBlock để lưu trữ khối tiếp theo
        public Block NextBlock {get; private set; }

        //Phương thức khởi tạo cho lớp BlockQueue
        public BlockQueue()
        {
            //Gắn khối ngẫu nhiên mới tạo cho khối tiếp theo
            NextBlock = RandomBlock();
        }

        //Phương thức trả về một khối ngẫu nhiên từ mảng Blocks
        private Block RandomBlock()
        {
            //Trả về một khối ngẫu nhiên từ mảng Blocks
            return blocks[random.Next(blocks.Length)];

        }
        //Phương thức trả về khối hiện tại và cập nhập khối tiếp theo
        public Block GetAndUpdate()
        {
            //Lưu trữ khối hiện tại vào biến block
            Block block = NextBlock;
            do
            {
                //Cập nhật khối tiếp theo
                NextBlock = RandomBlock();
            } while (block.Id == NextBlock.Id);//Lặp lại việc cập nhật khối tiếp theo cho đến khi khối tiếp theo không giống với khối hiện tại

            //Trả về khối hiện tại
            return block;
        }
    }
}
