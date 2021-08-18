
namespace BookBeing.Data
{
    public class DataConstants
    {
        public class BookConstants
        {
            public const int MaxLenghtTitle = 100;
            public const int MinLenghtTitle = 2;

            public const int MaxLenghtDescription = 20000;
            public const int MinLenghtDescription = 2;

            public const int MaxPrice = 10000;
            public const int MinPrice = 0;
        }

        public class AuthorConstants
        {
            public const int MaxLenghtName = 150;
            public const int MinLenghtName = 2;
        }

        public class PublisherConstants
        {
            public const int MaxLenghtName = 100;
            public const int MinLenghtName = 2;
        }

        public class LibraryConstants
        {
            public const int MaxLenghtName = 200;
            public const int MinLenghtName = 2;

            public const int MaxLenghtCity = 100;
            public const int MinLenghtCity = 4;

            public const int MaxLenghtZipCode = 5;
            public const int MinLenghtZipCode = 4;

            public const int MaxLenghtAddress = 400;
            public const int MinLenghtAddress = 5;

            public const int MaxLenghtPhone = 15;
            public const int MinLenghtPhone = 10;

        }
        public class UserConstants
        {
            public const int MaxLenghtName = 30;
            public const int MinLenghtName = 1;
        }

        public class AnnouncementConstants
        {
            public const int MaxLenghtText = 30000;
            public const int MinLenghtText = 10;
        }
    }
}
