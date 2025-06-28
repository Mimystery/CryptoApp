import { createContext } from "react";
import { useState, useEffect } from "react";
import { fetchCryptoWallet, fetchPrice, fetchSelectCoins } from '../api';
import { percentDifference } from '../utils'

export const CryptoContext = createContext({
    wallet: [],
    crypto: [],
    selectCoins: [],
    isAuthenticated: false,
    loading: false,
    setIsAuthenticated: () => {},
})

export const CryptoContextProvider = ({children}) => {
const [prices, setPrices] = useState([]);
const [wallet, setWallet] = useState([]);
const [loading, setLoading] = useState(true);
const [selectCoins, setSelectCoins] = useState([]);
const [isAuthenticated, setIsAuthenticated] = useState(false);

useEffect(() =>{
  const token = localStorage.getItem('jwt');
  if(token){
    setIsAuthenticated(true);
  }
  else{
    setIsAuthenticated(false);
  }
}, [])

  const fetchCryptoPrice = useCallback(async () => {
    let prices = await fetchPrice();
    let wallet = await fetchCryptoWallet();

    setWallet(wallet.map(walletCoin => {
      const coin = prices.find((c) => c.symbol === walletCoin.symbol)
      return {
        grow: walletCoin.price < coin.price,
        growPercent: percentDifference(walletCoin.price, coin.price),
        totalMoney: walletCoin.amount * coin.price,
        totalProfit: walletCoin.amount * coin.price - walletCoin.amount * walletCoin.price,
        ...walletCoin
      }
    }))
    setPrices(prices)
    setLoading(false)
  }, []);

  const fetchCryptoSelect = useCallback(async () => {
    setLoading(true)
    let selectCoins = await fetchSelectCoins();
    setSelectCoins(selectCoins)
    setLoading(false)
  }, []);

  useEffect(() => {
    if (isAuthenticated) {
      fetchCryptoSelect();
      fetchCryptoPrice();
    }
  }, [isAuthenticated, fetchCryptoPrice, fetchCryptoSelect]);

useEffect(() => {
    if (!isAuthenticated) return;

    const interval = setInterval(fetchCryptoPrice, 2000);
    return () => clearInterval(interval);
  }, [isAuthenticated, fetchCryptoPrice]);

    return(
    <CryptoContext.Provider value={{prices, wallet, loading, selectCoins, isAuthenticated}}>
        {children}
    </CryptoContext.Provider>
    )
}