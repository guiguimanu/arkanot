using Arkanot.GameLogic.PowerUps.Types;
namespace Arkanot.GameLogic.PowerUps
{
    public enum PowerUpEnum
    {
        None,
        WidePaddle,
        ShortPaddle,
        FastBall,
        SlowBall
    }

    public static class PowerUpTypes
    {
        /// <summary>
        /// Create an instance of a power up by enumerated type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IPowerUp Instantiate(PowerUpEnum type)
        {
            switch (type)
            {
                case PowerUpEnum.WidePaddle:
                    return new WidePaddle();
                case PowerUpEnum.ShortPaddle:
                    return new ShortPaddle();
                case PowerUpEnum.FastBall:
                    return new FastBall();
                case PowerUpEnum.SlowBall:
                    return new SlowBall();
                case PowerUpEnum.None:
                default:
                    return null;
            }
        }
    }
}
