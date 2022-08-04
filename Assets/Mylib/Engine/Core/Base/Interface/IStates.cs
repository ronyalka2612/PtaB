
namespace Com.GNL.URP_MyLib
{
    public interface IStates
    {
        // seperti Start pada mono behaviour, dibuat beda nama func agar mudah dipahami

        void MySttStart();
        void MySttEnable(bool isFindAll);
        void MySttUpdate();
        void MySttDisable();
        void MySttExit();
    }
}