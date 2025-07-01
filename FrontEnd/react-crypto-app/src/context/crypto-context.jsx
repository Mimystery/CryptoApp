import { createContext } from "react";
import { useState, useEffect } from "react";
import { addTransaction, fetchCryptoWallet, fetchPrice, fetchSelectCoins } from '../api';
import { isTokenExpired, percentDifference } from '../utils'

export const CryptoContext = createContext({
    wallet: [],
    crypto: [],
    selectCoins: [],
    isAuthenticated: false,
    user: null,
    loading: false,
})

export const CryptoContextProvider = ({children}) => {
const [prices, setPrices] = useState([]);
const [wallet, setWallet] = useState([]);
const [loading, setLoading] = useState(true);
const [selectCoins, setSelectCoins] = useState([]);
const [isAuthenticated, setIsAuthenticated] = useState(false);
const [user, setUser] = useState(null);
const [isInitialized, setIsInitialized] = useState(false);

const mapWallet = (wallet, prices) =>{
  if (!Array.isArray(wallet) || !Array.isArray(prices)) return [];
  return wallet.map(walletCoin => {
    const coin = prices.find(c => c.symbol.replace(/USDT$/, "").toLowerCase() === walletCoin.symbol);
    return {
      grow: coin ? walletCoin.price < coin.price : false,
      growPercent: coin ? percentDifference(walletCoin.price, coin.price) : 0,
      totalProfit: (coin.price - walletCoin.averagePrice) * walletCoin.totalAmount,
      ...walletCoin
    };
  })
}

useEffect(() =>{
  const token = localStorage.getItem('jwt');
  const savedUser = localStorage.getItem('userData')
  console.log("Context:" + savedUser);
  console.log('Token context:' + token);
  isTokenExpired(token)
  if(!isTokenExpired(token) && token && savedUser){
    setIsAuthenticated(true);
    setUser(JSON.parse(savedUser))
    console.log(isAuthenticated)
    console.log(user)
  }
  else{
    setIsAuthenticated(false);
    setUser(null)
    localStorage.removeItem('jwt');
    localStorage.removeItem('userData')
  }

  setIsInitialized(true)
}, [])

  useEffect(() => {
    
    const fetchCryptoPrice = async () =>{
        let prices = await fetchPrice();
        let wallet = await fetchCryptoWallet();

        console.log(wallet)
        setWallet(mapWallet(wallet, prices))
        setPrices(prices)
        setLoading(false)
    }

    fetchCryptoPrice()
    const interval = setInterval(fetchCryptoPrice, 2000); 

    return () => clearInterval(interval); 
  }, []);

  useEffect(() => {
    const token = localStorage.getItem('jwt');
    if (!isInitialized || !isAuthenticated) return;

    const fetchCryptoSelect = async () => {
        setLoading(true)
        let selectCoins = await fetchSelectCoins(token);
        setSelectCoins(selectCoins)
        setLoading(false)

    }
    fetchCryptoSelect()
  }, [isAuthenticated, isInitialized]);

  const addCoin = async (newCoin) => {
    let prices = await fetchPrice();
    try
    {
    await addTransaction(newCoin);
    setWallet((prev) => mapWallet([...prev, newCoin], prices))
    }
    catch(error){
      console.error(error)
    }
  }


    return(
    <CryptoContext.Provider value={{prices, wallet, loading, selectCoins, isAuthenticated, 
    setIsAuthenticated, user, setUser, setLoading, addCoin}}>
        {children}
    </CryptoContext.Provider>
    )
}