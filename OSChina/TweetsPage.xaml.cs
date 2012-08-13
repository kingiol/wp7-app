﻿using System;
using System. Collections. Generic;
using System. Linq;
using System. Net;
using System. Windows;
using System. Windows. Controls;
using System. Windows. Documents;
using System. Windows. Input;
using System. Windows. Media;
using System. Windows. Media. Animation;
using System. Windows. Shapes;
using Microsoft. Phone. Controls;
using OSChina. Model;
using OSChina. Model. AppOnly;

namespace OSChina
{
    public partial class TweetsPage : PhoneApplicationPage
    {
        public TweetsPage( )
        {
            InitializeComponent( );
            this. Loaded += (s, e) =>
                {
                    //if ( this.tweetListControl.listBoxHelper.allCount == 0 )
                    //{
                        this. tweetListControl. listBoxHelper_ReloadDelegate( );
                    //}
                };
        }
        /// <summary>
        /// 当前的动弹列表类型
        /// </summary>
        private TweetType Type = TweetType. Latest;

        protected override void OnNavigatedTo(System. Windows. Navigation. NavigationEventArgs e)
        {
            if ( this.NavigationContext.QueryString.ContainsKey("tag") )
            {
                switch ( this.NavigationContext.QueryString["tag"] )
                {
                    case "latest":
                        this. Type = TweetType. Latest;
                        this. tweetTitle. Text = "最新动弹";
                        break;
                    case "hottest":
                        this. Type = TweetType. Hottest;
                        this. tweetTitle. Text = "热门动弹";
                        break; 
                    case "my":
                        this. Type = TweetType. My;
                        this. tweetTitle. Text = "我的动弹";
                        break;
                }
                //注意这会导致动弹列表控件强制刷新
                this. tweetListControl. tweetType = this. Type;
            }
            base. OnNavigatedTo( e );
        }
        protected override void OnNavigatedFrom(System. Windows. Navigation. NavigationEventArgs e)
        {
            GC. Collect( );
            base. OnNavigatedFrom( e );
        }
        private void icon_Refresh_Click(object sender, EventArgs e)
        {
            if ( this.IsEnabled )
            {
                this. tweetListControl. Refresh( );
            }
        }

        private void icon_Tweet_Click(object sender, EventArgs e)
        {
            if ( this.IsEnabled && Tool. CheckLogin( ) )
            {
                Tool. To( "/PubTweetPage.xaml" );
            }
        }

        protected override void OnBackKeyPress(System. ComponentModel. CancelEventArgs e)
        {
            if ( this.tweetListControl != null )
            {
                this. tweetListControl. BackKeyPress(e );                
            }
            base. OnBackKeyPress( e );
        }
    }
}