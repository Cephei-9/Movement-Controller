# Movement-Controller

Это система движения для проекта Black Block шутера в духе CS

После написания движения я взялся за анимации для первого лица и во время работы над ними понял что не смогу написать контроллер персонажа сам в заданные сроки и в дальнейшенм работал через ассет UFPS, так что система движения осталось обрубком который удобно показать

### Описание

Я использовал ассет который удобно обернул классом оберткой(`FPCharracterController`). И нарастил функционал используя верхний класс (`MovementSystem`) как точку доступа к функционалу модуля движения для класса Local Player.

В перспективе там нужно было бы дописывать функционал и выстраивать различные режими движения, поэтому я и сделал классы с дополнительным функционалом небольшими чтобы их можно было удобно комбинировать и переиспользовать в будущем