import { useContext } from "react";
import { cryptoWallet } from "./data";
import axios from 'axios';
import { CryptoContext } from "./context/crypto-context";

const symbols = ["BTCUSDT", "ETHUSDT", "SOLUSDT", "BNBUSDT", "XRPUSDT"];
const ids = ['bitcoin', 'ethereum', 'solana', 'binancecoin', 'ripple'];

export const fetchPrice = async () => {
    try 
    {
      const responses = await Promise.all(symbols.map(symbol =>
            fetch(`https://api.binance.com/api/v3/ticker/price?symbol=${symbol}`)
            .then(res => res.json()))
        );
        return responses;
    } 
    catch (error) 
    {
      console.error("Ошибка при получении цены BTC:", error);
    } 
}

export const fetchCryptoWallet = async () =>{
const user = JSON.parse(localStorage.getItem('userData'))
  try
  {
    const wallet = await axios.get(`https://cryptoapp-foee.onrender.com/api/Telegram/user/${user?.id}/summary`)
    return wallet.data;
  }
  catch(e)
  { 
    console.error("Ошибка при получении wallet:", e);
  }
}

export const fetchSelectCoins = async (token) => {
  try 
    {
      const token = localStorage.getItem('jwt');
      const response = await fetch(`https://cryptoapp-foee.onrender.com/api/Coin/list`,{
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`,
      },
    });

    if (!response.ok) {
      throw new Error('Ошибка авторизации');
    }

      const data = await response.json(); 
      return data;
    }
    catch (error) 
    {
      console.error("Ошибка при получении списка криптовалют:", error);
      return [];
    } 
}

export const addTransaction = async (coin) => {
  const user = JSON.parse(localStorage.getItem('userData'))
  try 
    {
      var response = await axios.post(`https://cryptoapp-foee.onrender.com/api/Telegram/user/${user?.id}/transaction`, coin)
      return response.status
    }
    catch (error) 
    {
      console.error(error);
    } 
}

export const fetchAllTransactions = async () => {
  const user = JSON.parse(localStorage.getItem('userData'))
  try 
  {
    var response = await axios.get(`https://cryptoapp-foee.onrender.com/api/Telegram/user/${user?.id}/transaction`)
    return response.data;
  }
  catch (error) 
  {
    console.error(error);
  } 
}