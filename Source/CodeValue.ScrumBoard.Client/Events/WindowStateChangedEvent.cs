using CodeValue.ScrumBoard.Client.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Client.Events
{
    public sealed class WindowStateChangedEvent
    {
        private readonly WindowStateType _windowStateType;
        public WindowStateType WindowStateType => _windowStateType;
        public WindowStateChangedEvent(WindowStateType windowStateType)
        {
            _windowStateType = windowStateType;
        }
    }
}
