using BaseCore.Validation.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseCore.Validation.Validations
{
    public partial class Contract : Notifiable
    {
        public Contract Requires()
        {
            return this;
        }

        public Contract Join(params Notifiable[] items)
        {
            if (items != null)
            {
                foreach (var notifiable in items)
                {
                    if (notifiable.Invalid)
                        AddNotifications(notifiable.Notifications);
                }
            }

            return this;
        }

        public Contract IfNotNull(object parameterType, Expression<Func<Contract, Contract>> contractExpression)
        {
            if (parameterType != null)
            {
                contractExpression.Compile().Invoke(this);
            }

            return this;
        }
    }
}
