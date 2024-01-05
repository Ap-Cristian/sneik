using Logic.Models;
using Microsoft.Xna.Framework.Input;
using sneik.Interfaces;

namespace sneik
{
    public class InputHandler: IUpdateable
    {
        private KeyboardState _keyboardState;
        private SneikGameUseCase _sneikUseCase;
        public InputHandler()
        {
            _sneikUseCase = SneikGameUseCase.Instance;
        }

        public void Update()
        {
            _keyboardState = Keyboard.GetState();
            Keys[] pressedKeys = _keyboardState.GetPressedKeys();
            if(pressedKeys.Length > 0)
            {
                _sneikUseCase.KeyPressed(sneikTools.Tools.XnaKeysToLogicKeys(pressedKeys));
            }
        }
    }
}
