using Tony.Listener.Tcp;

namespace Tony.Listener.Composers.Player;
internal class RightsComposer : ComposerBase {
    public override short Header => 2;

    public override Message Compose() {
        Message msg = base.Compose();
        msg.Write( "fuse_login" );
        msg.Write( "fuse_trade" );
        msg.Write( "fuse_buy_credits" );
        msg.Write( "fuse_buy_credits_fuse_login" );
        msg.Write( "fuse_room_queue_default" );
        msg.Write( "fuse_mute" );
        msg.Write( "fuse_kick" );
        msg.Write( "fuse_receive_calls_for_help" );
        msg.Write( "fuse_remove_photos" );
        msg.Write( "fuse_remove_stickies" );
        msg.Write( "fuse_mod" );
        msg.Write( "fuse_moderator_access" );
        msg.Write( "fuse_chat_log" );
        msg.Write( "fuse_room_alert" );
        msg.Write( "fuse_room_kick" );
        msg.Write( "fuse_ignore_room_owner" );
        msg.Write( "fuse_enter_full_rooms" );
        msg.Write( "fuse_enter_locked_rooms" );
        msg.Write( "fuse_see_all_roomowners" );
        msg.Write( "fuse_search_users" );
        msg.Write( "fuse_ban" );
        msg.Write( "fuse_see_chat_log_link" );
        msg.Write( "fuse_cancel_roomevent" );
        msg.Write( "fuse_administrator_access" );
        msg.Write( "fuse_any_room_controller" );
        msg.Write( "fuse_pick_up_any_furni" );
        msg.Write( "fuse_see_flat_ids" );
        msg.Write( "fuse_credits" );
        msg.Write( "fuse_housekeeping_alert" );
        msg.Write( "fuse_habbo_chooser" );
        msg.Write( "fuse_performance_panel" );
        return msg;
    }
}
