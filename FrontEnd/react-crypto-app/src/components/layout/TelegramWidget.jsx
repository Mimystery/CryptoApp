import { useEffect } from 'react';

export default function TelegramWidget() {
  useEffect(() => {
    // Глобальная функция, которую вызывает Telegram
    window.onTelegramAuth = function (user) {
      alert(
        'Logged in as ' +
          user.first_name +
          ' ' +
          user.last_name +
          ' (' +
          user.id +
          (user.username ? ', @' + user.username : '') +
          ')'
      );

      console.log(user)

    //   fetch('https://cryptoapp-foee.onrender.com/api/Telegram/auth', {
    //     method: 'POST',
    //     headers: {
    //       'Content-Type': 'application/json',
    //     },
    //     body: JSON.stringify(user),
    //   })
    //     .then((res) => res.json())
    //     .then((data) => {
    //       console.log('🔵 Ответ от сервера:', data);
    //     })
    //     .catch((err) => {
    //       console.error('❌ Ошибка отправки:', err);
    //     });
    };

    // Создаём скрипт Telegram
    const script = document.createElement('script');
    script.src = 'https://telegram.org/js/telegram-widget.js?22';
    script.async = true;
    script.setAttribute('data-telegram-login', 'cryptoappservicebot');
    script.setAttribute('data-size', 'medium');
    script.setAttribute('data-radius', '10');
    script.setAttribute('data-request-access', 'write');
    script.setAttribute('data-onauth', 'onTelegramAuth(user)');

    const container = document.getElementById('telegram-login');
    if (container) {
      container.innerHTML = '';
      container.appendChild(script);
    }

    return () => {
      delete window.onTelegramAuth;
      if (container) container.innerHTML = '';
    };
  }, []);

  return (
    <div
      id="telegram-login"
      style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '50px' }}
    />
  );
}
