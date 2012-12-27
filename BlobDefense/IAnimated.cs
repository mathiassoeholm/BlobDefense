namespace BlobDefense
{
    internal interface IAnimated
    {
        Animation CurrentAnimation { get; }
        
        void RunAnimation();
    }
}
