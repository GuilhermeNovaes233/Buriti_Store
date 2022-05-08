using System;

namespace Buriti_Store.Orders.Domain.Enums
{
    public enum OrderStatus
    {
        Sketch = 0,
        Initiated = 1,
        PaidOut = 4,
        Delivered = 5,
        Canceled = 6
    }
}