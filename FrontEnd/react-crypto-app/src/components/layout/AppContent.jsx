import { Layout, Tabs } from 'antd';
import { useContext, useEffect, useState } from "react";
import { CryptoContext } from '../../context/crypto-context';
import TradingViewWidget from './TradingViewWidget';
import WalletWidget from './WalletWidget';

const contentStyle = {
  textAlign: 'center',
  height: 'calc(100vh - 60px)',
  color: '#fff',
  backgroundColor: '#f2f5ff',
  padding: '1rem',
};

const tabContentStyle = {
  flex: 1,
  height: '100%',
  display: 'flex',
  flexDirection: 'column',
};

const items = [
  {
    key: '1',
    label: 'Charts',
    children: (
      <div style={tabContentStyle}><TradingViewWidget/></div>
    ),
  },
  {
    key: '2',
    label: 'Wallet',
    children: (
      <div style={tabContentStyle}><WalletWidget/></div>
    ),
  },
];

export default function AppContent(){
const {prices, loading} = useContext(CryptoContext)

    return(
        <Layout.Content style={contentStyle}>
    <Tabs items={items} style={{height: '100%'}}></Tabs>
    
        </Layout.Content>
    )
}