import React, { useEffect } from 'react';

export default function TelegramWidget() {
  useEffect(() => {
    // Объявляем функцию прямо на window один раз
    window.handleTelegramAuth = function(user) {
      console.log("🟢 Получены данные от Telegram:", user);
      alert(`Привет, ${user.first_name} (@${user.username})!`);
    };

    const script = document.createElement('script');
    script.src = 'https://telegram.org/js/telegram-widget.js?22'; // лучше взять версию с ?22
    script.async = true;
    script.setAttribute('data-telegram-login', 'cryptoappservicebot'); // твой бот
    script.setAttribute('data-size', 'medium');
    script.setAttribute('data-radius', '10');
    script.setAttribute('data-request-access', 'write');
    script.setAttribute('data-userpic', 'false');
    script.setAttribute('data-onauth', 'handleTelegramAuth');

    const container = document.getElementById('telegram-login-button');
    if (container) {
      container.innerHTML = '';
      container.appendChild(script);
    }

    // Очистка (опционально)
    return () => {
      delete window.handleTelegramAuth;
      if (container) container.innerHTML = '';
    };
  }, []);

  return (
    <div
      id="telegram-login-button"
      style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '50px' }}
    />
  );
}
